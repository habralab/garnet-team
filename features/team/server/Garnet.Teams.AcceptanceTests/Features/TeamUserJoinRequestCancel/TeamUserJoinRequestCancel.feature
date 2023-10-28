Функция: 
    Я, как пользователь 
    Хочу иметь возможность отменять заявку на вступление в другую команду
    Чтобы в случае формирования заявки не в ту команду иметь возможность отменить свой выбор

    Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И существует заявка на вступление в команду 'FooBar' от пользователя 'Вася'

        Сценарий: Отмена заявки на вступление в команду
            Допустим существует пользователь 'Петя'
            И владельцем команды 'FooBar' является 'Петя'
            Когда 'Вася' отменяет заявку пользователя 'Вася' на вступление в 'FooBar'
            Тогда в команде 'FooBar' количество заявок на вступление равно '0'
            И для пользователя 'Петя' существует уведомление типа 'TeamUserJoinRequestCancel'
            И в последнем уведомлении для пользователя 'Петя' связанной сущностью является пользователь 'Вася'

        Сценарий: Пользователь может отменять только свои заявки на вступление в команду
            Допустим существует пользователь 'Петя'
            Когда 'Петя' отменяет заявку пользователя 'Вася' на вступление в 'FooBar'
            Тогда пользователь получает ошибку 'TeamUserJoinRequestOnlyAuthorCanCancelError'