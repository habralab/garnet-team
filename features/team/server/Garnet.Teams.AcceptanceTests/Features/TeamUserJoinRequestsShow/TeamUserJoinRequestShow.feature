Функция:
    Я, как пользователь-владелец команды
    Хочу иметь возможность посмотреть существующие заявки на вступление в свою команду
    Чтобы фильтровать участников своего сообщества
            
        Сценарий: Просмотр заявки на вступление в команду
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И существует пользователь 'Маша'
            И существует заявка на вступление в команду 'FooBar' от пользователя 'Маша'
            Когда 'Вася' просматривает заявки на вступление в команду 'FooBar'
            Тогда в списке отображается '1' заявка