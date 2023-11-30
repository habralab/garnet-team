Функция: 
    Я, как пользователь
    Хочу иметь возможность принимать или отклонять приглашения на вступление в команду
    Чтобы фильтровать свои сообщества

        Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И существует пользователь 'Маша'
            И существует приглашение пользователя 'Маша' на вступление в команду 'FooBar' от владельца

        Сценарий: Прием приглашения в команду
            Когда 'Маша' принимает приглашение в команду 'FooBar'
            Тогда в команде 'FooBar' количество участников равно '2'
            И для пользователя 'Вася' существует уведомление типа 'TeamInviteDecide'
            И в последнем уведомлении для пользователя 'Вася' связанной сущностью является пользователь 'Маша'
            И для пользователя 'Маша' нет уведомлений типа 'TeamInvite'
            И для пользователя 'Маша' нет уведомлений со связанной сущностью командой 'FooBar'

        Сценарий: Отказ приглашения в команду
            Когда 'Маша' отклоняет приглашение в команду 'FooBar'
            Тогда в команде 'FooBar' количество участников равно '1'
            И для пользователя 'Вася' существует уведомление типа 'TeamInviteDecide'
            И в последнем уведомлении для пользователя 'Вася' связанной сущностью является пользователь 'Маша'
            И для пользователя 'Маша' нет уведомлений типа 'TeamInvite'
            И для пользователя 'Маша' нет уведомлений со связанной сущностью командой 'FooBar'