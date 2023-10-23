Функция: 
    Я, как пользователь 
    Хочу иметь возможность отменять приглашение на вступление в свою команду
    Чтобы в случае формирования приглашения не тому пользователю иметь возможность отменить свой выбор

    Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И пользователь 'Вася' является участником команды 'FooBar'
            И существует пользователь 'Маша'
            И существует приглашение пользователя 'Маша' на вступление в команду 'FooBar' от владельца

        Сценарий: Отмена приглашения на вступление в свою команду
            Когда 'Вася' отменяет приглашение пользователя 'Маша' на вступление в команду 'FooBar'
            Тогда в команде 'FooBar' количество приглашений на вступление равно '0'

        Сценарий: Отменять приглашения на вступление в свою команду может только владелец
            Когда 'Маша' отменяет приглашение пользователя 'Маша' на вступление в команду 'FooBar'
            Тогда пользователь получает ошибку 'TeamJoinInvitationOnlyOwnerCanCancelError'
