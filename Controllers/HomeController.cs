using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SouvenirShop.Models;
using System.Text.Json;
using SouvenirShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace SouvenirShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var souvenirs = _context.Souvenirs.ToList();
        return View(souvenirs);
    }

    [HttpGet]
    public async Task<IActionResult> SearchTovar(string SearchedTovar)
    {
        ViewBag.SearchTerm = SearchedTovar;

        if (string.IsNullOrWhiteSpace(SearchedTovar))
        {
            return View(new List<Souvenir>());
        }

        var lowerTerm = SearchedTovar.Trim().ToLower();

        var results = await _context.Souvenirs
            .Where(p => p.name.ToLower().Contains(lowerTerm) || 
                        p.Description.ToLower().Contains(lowerTerm))
            .AsNoTracking()
            .ToListAsync();

        return View(results);
    }

    [HttpPost]
    public IActionResult AddObjectToList(int Id)
    {
        var obj = _context.Souvenirs.ToList().FirstOrDefault(o => o.id == Id);
        if(obj != null)
        {
            int[]? new_mass = JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("corzina") ?? "[-1]");
            
            if(new_mass == null)
            {
                new_mass = [0];
            }
            Array.Resize(ref new_mass, new_mass.Length + 1);
            new_mass[new_mass.Length - 1] = obj.id ?? -1;

            HttpContext.Session.SetString("corzina", JsonSerializer.Serialize(new_mass));
        }
        return RedirectToAction(nameof(Index));
    }
    

    
    public IActionResult Corzina()
    {
        var souvenirs = _context.Souvenirs.ToList();
        var my_souvenirs = new List<Souvenir>();
        int[] new_mass = JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("corzina") ?? "[-1]");
        if(new_mass == null)
        {
            new_mass = [];
        }
        for(int i = 0; i < new_mass.Length; i = i + 1)
        {
            for(int j = 0; j < souvenirs.Count; j = j + 1)
            {
                if(new_mass[i] == souvenirs[j].id)
                {
                    my_souvenirs.Add(souvenirs[j]);
                }
            }
        }
        return View(my_souvenirs);
    }
    [HttpGet]
    public IActionResult Order(int Id)
    {
        HttpContext.Session.SetInt32("current_order", Id);
        var souvenirs = _context.Souvenirs.ToList();
        var current_spisok_in_order = _context.Orders.Find(Id).Souvenirs_id;
        var my_souvenirs = new List<Souvenir>();
        if(current_spisok_in_order == null)
        {
            current_spisok_in_order = [];
        }
        for(int i = 0; i < current_spisok_in_order.Count(); i = i + 1)
        {
            for(int j = 0; j < souvenirs.Count; j = j + 1)
            {
                if(current_spisok_in_order[i] == souvenirs[j].id)
                {
                    Console.WriteLine("Список: " + current_spisok_in_order[i]);
                    my_souvenirs.Add(souvenirs[j]);
                }
            }
        }
        return View(my_souvenirs);
    }
    public IActionResult Orders()
    {
        var orders = _context.Orders.ToList();
        
        return View(orders);
    }
    public IActionResult My_Orders()
    {
        var orders = _context.Orders.ToList();
        var my_orders = new List<Order>();
        for(int i = 0; i < orders.Count; i++)
        {
            if(orders[i].User_id == HttpContext.Session.GetInt32("id"))
            {
                my_orders.Add(orders[i]);
            }
        }

        return View(my_orders);
    }
    [HttpPost]
    public IActionResult Create_order()
    {
        int[] corzina = JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("corzina") ?? "[-1]");
        
        int? now_id = 0;
        if(_context.Orders.Count() != 0){
            now_id = _context.Orders.Last().id + 1;
        }
        Order NewOrder = new Order
        {
            id = now_id,
            User_id = HttpContext.Session.GetInt32("id"),
            Souvenirs_id = corzina,
            Order_status = "Новый"
        };


        int?[] order_helper_mass = JsonSerializer.Deserialize<int?[]>(HttpContext.Session.GetString("my_order") ?? "[-1]");
        order_helper_mass.Append(now_id);
        HttpContext.Session.SetString("my_order", JsonSerializer.Serialize(order_helper_mass));

        HttpContext.Session.SetString("corzina", "[-1]");
        _context.Orders.AddRange(NewOrder);
        _context.SaveChanges();
        return RedirectToAction("Corzina");

    }
    [HttpPost]
    public IActionResult View_order(int Id)
    {
        int ID = Id;
        return RedirectToAction("Order", new { Id = ID });
    }

    public void Send_mail(string my_subject, string my_message, User user)
    {
            string fromMail = "osipov0063@gmail.com";
            string fromPassword = "jjzauvlgbdndtlza";
            string toMail = user.Email;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = my_subject;
            message.Body = my_message;
            message.To.Add(new MailAddress(toMail));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);

            try
            {
                
                smtpClient.Send(message);
                Console.WriteLine("Письмо успешно отправлено!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки: {ex.Message}");
                
            }
    }

    [HttpPost]
    public IActionResult SetStatus_new(int Id)
    {
        var order = _context.Orders.Find(Id);
        order.Order_status = "Новый";
        _context.Orders.Update(order);
        _context.SaveChanges();

        var user = _context.Users.ToList().FirstOrDefault(u => u.id == order.User_id);
        Send_mail("Оповещение о товаре", "Здравствуйте, " + user.Username + ", ваш заказ на SouvenirShop получил статус 'Новый'.", user);

        return RedirectToAction("Orders");
    }
    [HttpPost]
    public IActionResult SetStatus_wait(int Id)
    {
        var order = _context.Orders.Find(Id);
        order.Order_status = "В обработке";
        _context.Orders.Update(order);
        _context.SaveChanges();

        var user = _context.Users.ToList().FirstOrDefault(u => u.id == order.User_id);
        Send_mail("Оповещение о товаре", "Здравствуйте, " + user.Username + ", ваш заказ на SouvenirShop получил статус 'В обработке'.", user);

        return RedirectToAction("Orders");
    }
    [HttpPost]
    public IActionResult SetStatus_rejected(int Id)
    {
        var order = _context.Orders.Find(Id);
        order.Order_status = "Отклонён";
        _context.Orders.Update(order);
        _context.SaveChanges();

        var user = _context.Users.ToList().FirstOrDefault(u => u.id == order.User_id);
        Send_mail("Оповещение о товаре", "Здравствуйте, " + user.Username + ", ваш заказ на SouvenirShop получил статус 'Отклонён'.", user);

        return RedirectToAction("Orders");
    }
    [HttpPost]
    public IActionResult SetStatus_success(int Id)
    {
        var order = _context.Orders.Find(Id);
        order.Order_status = "Выполнен";
        _context.Orders.Update(order);
        _context.SaveChanges();

        var user = _context.Users.ToList().FirstOrDefault(u => u.id == order.User_id);
        Send_mail("Оповещение о товаре", "Здравствуйте, " + user.Username + ", ваш заказ на SouvenirShop получил статус 'Выполнен'.", user);

        return RedirectToAction("Orders");
    }

    

    [HttpPost]
    public IActionResult RemoveObjectToList(int Id)
    {
        var obj = _context.Souvenirs.ToList().FirstOrDefault(o => o.id == Id);
        if(obj != null)
        {
            int[]? new_mass = JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("corzina") ?? "[-1]");
            
            if(new_mass == null)
            {
                new_mass = [0];
            }
            int check = 0;
            for(int i = 0; i < new_mass.Length - 1; i++)
            {
                if(new_mass[i] == Id)
                {
                    new_mass[i] = -1;
                    (new_mass[i + 1], new_mass[i]) = (new_mass[i], new_mass[i + 1]);
                    check = 1;
                }
                else if(check == 1)
                {
                    (new_mass[i + 1], new_mass[i]) = (new_mass[i], new_mass[i + 1]);
                }
            }
            Array.Resize(ref new_mass, new_mass.Length - 1);

            HttpContext.Session.SetString("corzina", JsonSerializer.Serialize(new_mass));
            _context.Users.FirstOrDefault(u => u.id == HttpContext.Session.GetInt32("id")).Corzina = new_mass;
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Corzina));
    }
    [HttpPost]
    public IActionResult Add_Tovar_to_site(Souvenir model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        string localPathForTovar = "";

        if (model.ImageFile != null)
        {
            Console.WriteLine("Файл прошёл " + localPathForTovar);
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);

            string physicalSavePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = new FileStream(physicalSavePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(fileStream);
            }

            localPathForTovar = "/images/" + uniqueFileName;
        }

        
        var now_s = _context.Souvenirs.OrderBy(u => u.id).Skip(_context.Souvenirs.Count() - 1).FirstOrDefault();

        Souvenir NewTovar = new Souvenir
        {
            id = now_s.id + 1,
            name = model.name,
            Price = model.Price,
            Description = model.Description,
            ImageUrl = localPathForTovar
        };

        _context.Souvenirs.AddRange(NewTovar);
        _context.SaveChanges();
        Console.WriteLine("Функция прошла " + localPathForTovar);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddReview(int Id)
    {
        HttpContext.Session.SetInt32("remember_id", Id);

        var product = await _context.Souvenirs.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.id == Id);

        if (product == null) return NotFound();

        ViewBag.Product = product;
        ViewBag.ExistingReviews = product.Reviews.OrderByDescending(r => r.CreatedAt).ToList();

        var model = new Review { ProductId = Id, Sou = product };
        return View(model);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add_Review(Review review)
    {
        review.CreatedAt = DateTime.UtcNow;

        var product = await _context.Souvenirs.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.id == review.ProductId);
        if (ModelState.IsValid)
        {
            if (product.Reviews == null)
            {
                product.Reviews = new List<Review>();
            }
            product.Reviews.Add(review);

            _context.Reviews.Add(review);
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(AddReview), new { id = review.ProductId });
        }
        
        if (product == null) return NotFound();

        ViewBag.Product = product;
        ViewBag.ExistingReviews = product.Reviews.OrderByDescending(r => r.CreatedAt).ToList();
        
        return View("AddReview", review);
    }

    [HttpPost]
    public IActionResult Delete_Tovar_to_site(int id)
    {
        var souvenir = _context.Souvenirs.Find(id);

        if (souvenir != null)
        {
            _context.Souvenirs.Remove(souvenir);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        return View();
    }
    [HttpGet]
    public IActionResult Remake_tovar(int id)
    {
        var souvenir = _context.Souvenirs.Find(id);
        return View(souvenir);
    }
    [HttpPost]
    public IActionResult Remake_tovar_to_site(int id, Souvenir souvenir)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", souvenir); 
        }
        var current_souvenir = _context.Souvenirs.Find(id);
        current_souvenir.Description = souvenir.Description;
        current_souvenir.name = souvenir.name;
        current_souvenir.Price = souvenir.Price;

        string localPathForTovar = current_souvenir.ImageUrl;

        if (souvenir.ImageFile != null)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(souvenir.ImageFile.FileName);

            string physicalSavePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = new FileStream(physicalSavePath, FileMode.Create))
            {
                souvenir.ImageFile.CopyTo(fileStream);
            }

            localPathForTovar = "/images/" + uniqueFileName;
        }
        current_souvenir.ImageUrl = localPathForTovar;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult CreateTovar()
    {
        return View();
    }
    public IActionResult Profile(){
        User model = new User
        {
            Username = HttpContext.Session.GetString("username"),
            id = HttpContext.Session.GetInt32("id"),
            Email = HttpContext.Session.GetString("Email"),
            Password = "",
            Corzina = [-1]
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(Review review)
    {
        if (ModelState.IsValid)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var refererUrl = Request.Headers["Referer"].ToString();
            return Redirect(refererUrl);
        }
        var product = await _context.Souvenirs
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.id == review.ProductId);
        return View("Details", product);
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user){
        
        if (ModelState.IsValid){
            if(HttpContext.Session.GetInt32("id") == null)
            {
                user.Corzina = JsonSerializer.Deserialize<int[]>(HttpContext.Session.GetString("corzina") ?? "[-1]");
                Console.WriteLine(user.Corzina[0]);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Login");
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string Email, string password){
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == password);
        if(user != null){
            string fromMail = "osipov0063@gmail.com";
            string fromPassword = "jjzauvlgbdndtlza";
            string toMail = user.Email;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Проверка личности.";
            Random ran = new Random();
            int checker_number = ran.Next(100000, 999999);
            message.Body = "Здравствуйте, " + user.Username + ", вы, вероятно, пытаетесь войти в свой аккаунт на SouvenirShop. Вот ваш код подтверждения: " + checker_number + ".";
            message.To.Add(new MailAddress(toMail));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);

            try
            {
                HttpContext.Session.SetInt32("id_bufer", user.id ?? 0);
                HttpContext.Session.SetString("username_bufer", user.Username ?? "_");
                HttpContext.Session.SetString("Email_bufer", user.Email ?? "_");
                HttpContext.Session.SetString("corzina_bufer", JsonSerializer.Serialize(user.Corzina));

                smtpClient.Send(message);
                Console.WriteLine("Письмо успешно отправлено!");
                HttpContext.Session.SetInt32("checker_number", checker_number);
                return RedirectToAction("Auth_check", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки: {ex.Message}");
                
            }


            
            //Console.WriteLine(HttpContext.Session.GetString("corzina"));
            
        }
        return View();
    }

    

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("id");
        HttpContext.Session.Remove("username");
        HttpContext.Session.Remove("Email");
        HttpContext.Session.SetString("corzina", JsonSerializer.Serialize(new int[1] {-1}));

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Auth_check()
    {
        return View();
    }

    [HttpPost]
    public IActionResult VerifyCode(string checker_number)
    {
        if (string.IsNullOrWhiteSpace(checker_number))
        {
            ModelState.AddModelError("", "Код не может быть пустым.");
            return View("Verify");
        }

        

        if (checker_number == HttpContext.Session.GetInt32("checker_number").ToString())
        {
            HttpContext.Session.SetInt32("id", HttpContext.Session.GetInt32("id_bufer") ?? 0);
            HttpContext.Session.SetString("username", HttpContext.Session.GetString("username_bufer"));
            HttpContext.Session.SetString("Email", HttpContext.Session.GetString("Email_bufer"));
            HttpContext.Session.SetString("corzina", HttpContext.Session.GetString("corzina_bufer"));

            return RedirectToAction("Index", "Home");
        }

        ViewBag.ErrorMessage = "Неверный код подтверждения.";
        return View();
    }
}
