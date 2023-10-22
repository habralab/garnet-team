Функция: 
    Я, как пользователь-владелец команды
    Хочу иметь возможность видеть как давно висит приглашение пользователя в команду
    Чтобы получить понимание об актуальности приглашения

        Сценарий: Дата и время подачи заявки пользователя на вступление в команду отображается корректно
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И существует пользователь 'Маша'
            И существует пользователь 'Петя'
            И существует заявка на вступление в команду 'FooBar' от пользователя 'Петя'
            И заявка на вступление в команду 'FooBar' от пользователя 'Петя' была создана '2023-07-01'
            И существует заявка на вступление в команду 'FooBar' от пользователя 'Маша'
            И заявка на вступление в команду 'FooBar' от пользователя 'Маша' была создана '2023-10-01'
            Когда 'Вася' просматривает заявки на вступление в команду 'FooBar' в порядке актуальности
            Тогда дата создания первой заявки в списке равна '2023-10-01'