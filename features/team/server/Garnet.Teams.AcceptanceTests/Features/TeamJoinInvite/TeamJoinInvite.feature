Функция: 
    Я, как пользователь-владелец команды
    Хочу иметь возможность приглашать других пользователей в свою команду
    Чтобы формировать свое сообщество самостоятельно

    Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И существует пользователь 'Маша'

        Сценарий: Приглашение пользователя в свою команду
            Когда пользователь 'Вася' приглашает 'Маша' в команду 'FooBar'
            Тогда у пользователя 'Маша' количество приглашений в команды равно '1'
            И для пользователя 'Маша' существует уведомление типа 'TeamInvite'
            И в последнем уведомлении для пользователя 'Маша' связанной сущностью является команда 'FooBar'

        Сценарий: Приглашение участника в свою команду
            Допустим пользователь 'Маша' является участником команды 'FooBar'
            Когда пользователь 'Вася' приглашает 'Маша' в команду 'FooBar'
            Тогда у пользователя 'Маша' количество приглашений в команды равно '0'
            И пользователь получает ошибку 'TeamUserIsAlreadyAParticipantError'

        Сценарий: Приглашение пользователя с существующей заявкой на вступление от пользователя
            Допустим существует заявка на вступление в команду 'FooBar' от пользователя 'Маша'
            Когда пользователь 'Вася' приглашает 'Маша' в команду 'FooBar'
            Тогда у пользователя 'Маша' количество приглашений в команды равно '0'
            И пользователь получает ошибку 'TeamPendingUserJoinRequestError'

        Сценарий: Приглашение пользователя с существующей заявкой на вступление от владельца
            Допустим существует приглашение пользователя 'Маша' на вступление в команду 'FooBar' от владельца
            Когда пользователь 'Вася' приглашает 'Маша' в команду 'FooBar'
            Тогда у пользователя 'Маша' количество приглашений в команды равно '1'
            И пользователь получает ошибку 'TeamPendingJoinInvitationError'