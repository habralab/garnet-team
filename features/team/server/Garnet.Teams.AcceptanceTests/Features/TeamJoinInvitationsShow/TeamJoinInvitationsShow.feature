Функция: 
    Я, как пользователь-владелец команды
    Хочу иметь возможность просматривать приглашения пользователей в свою команду
    Чтобы понимать кому я уже отправил приглашения на вступление в команду
    
        Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И существует пользователь 'Маша'

        Сценарий: Просмотр приглашений пользователей для команды
            Допустим существует приглашение пользователя 'Маша' на вступление в команду 'FooBar' от владельца
            Когда 'Вася' просматривает список приглашений команды 'FooBar'
            Тогда количество приглашений в списке равно '1'

        Сценарий: Просмотр приглашений пользователей доступен только владельцу
            Допустим существует приглашение пользователя 'Маша' на вступление в команду 'FooBar' от владельца
            Когда 'Маша' просматривает список приглашений команды 'FooBar'
            Тогда пользователь получает ошибку 'TeamJoinInvitationOnlyOwnerCanSeeError'