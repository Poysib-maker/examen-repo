using SouvenirShop.Data;

namespace SouvenirShop.Models;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context){
        if(context.Souvenirs.Any()){
            return;
        }
        context.Souvenirs.AddRange(
            new Souvenir {id = 1, name = "Магнит", Price = 50, Description = "Красный магнит",      ImageUrl = "/images/magnet1.jpeg"},
            new Souvenir {id = 2, name = "Магнит", Price = 50, Description = "Синий магнит",        ImageUrl = "/images/magnet2.jpeg"},
            new Souvenir {id = 3, name = "Магнит", Price = 50, Description = "Зелёный магнит",      ImageUrl = "/images/magnet3.jpeg"},
            new Souvenir {id = 4, name = "Магнит", Price = 50, Description = "Жёлтый магнит",       ImageUrl = "/images/magnet4.jpeg"},
            new Souvenir {id = 5, name = "Магнит", Price = 50, Description = "Фиолетовый магнит",   ImageUrl = "/images/magnet5.jpeg"},

            new Souvenir {id = 6, name = "Кружка", Price = 200, Description = "Кружка с видом города с горы",                                                   ImageUrl = "/images/cup1.jpeg"},
            new Souvenir {id = 7, name = "Кружка", Price = 200, Description = "Кружка с видом на море",                                                         ImageUrl = "/images/cup2.jpeg"},
            new Souvenir {id = 8, name = "Кружка", Price = 200, Description = "Кружка с рельефным узором города. Изображены очертания достопримечательностей",  ImageUrl = "/images/cup3.jpeg"},
            new Souvenir {id = 9, name = "Кружка", Price = 200, Description = "Кружка с географическим рельефом области",                                       ImageUrl = "/images/cup4.jpeg"},
            new Souvenir {id = 10, name = "Кружка", Price = 200, Description = "Кружка с видом города",                                                         ImageUrl = "/images/cup5.jpeg"},

            new Souvenir {id = 11, name = "Статуэтка", Price = 3500, Description = "Маленькая статуэтка. Приносит удачу",   ImageUrl = "/images/statue.jpeg"},
            new Souvenir {id = 12, name = "Ракушка", Price = 200, Description = "Шипастая ракушка 'Ёжик', средняя",         ImageUrl = "/images/rakushka1.jpeg"},
            new Souvenir {id = 13, name = "Ракушка", Price = 350, Description = "Шипастая ракушка 'Ёжик', крупная",         ImageUrl = "/images/rakushka2.jpeg"},

            new Souvenir {id = 14, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Иван' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 15, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Александр' с гравюровкой",   ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 16, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Михаил' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 17, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Дмитрий' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 18, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Сергей' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 19, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Алексей' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 20, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Андрей' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 21, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Евгений' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 22, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Максим' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 23, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Николай' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},

            new Souvenir {id = 24, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Владимир' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 25, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Павел' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 26, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Денис' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 27, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Антон' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 28, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Роман' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 29, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Олег' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 30, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Артём' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 31, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Кирилл' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 32, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Григорий' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 33, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Тимофей' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},

            new Souvenir {id = 34, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Ярослав' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 35, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Константин' с гравюровкой",  ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 36, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Егор' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 37, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Данил' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 38, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Степан' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 39, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Анна' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 40, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Мария' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 41, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Екатерина' с гравюровкой",   ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 42, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Ольга' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 43, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Татьяна' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},

            new Souvenir {id = 44, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Елена' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 45, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Наталья' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 46, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Ирина' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 47, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Юлия' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 48, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Виктория' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 49, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Дарья' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 50, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Ксения' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 51, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Александра' с гравюровкой",  ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 52, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Анастасия' с гравюровкой",   ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 53, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'София' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},

            new Souvenir {id = 54, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Светлана' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 55, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Валерия' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 56, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Елизавета' с гравюровкой",   ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 57, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Алёна' с гравюровкой",       ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 58, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Вера' с гравюровкой",        ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 59, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Ульяна' с гравюровкой",      ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 60, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Вероника' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 61, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Кристина' с гравюровкой",    ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 62, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Евгения' с гравюровкой",     ImageUrl = "/images/lojka.jpeg"},
            new Souvenir {id = 63, name = "Ложка", Price = 700, Description = "Именная позолоченная ложка 'Маргарита' с гравюровкой",   ImageUrl = "/images/lojka.jpeg"}
        );
        context.Users.AddRange(new User {id = 0, Email = "osipov0063@gmail.com", Username = "Administrator_man", Corzina = [-1], Password = "try_the_anything"});

        context.SaveChanges();
    }
}
