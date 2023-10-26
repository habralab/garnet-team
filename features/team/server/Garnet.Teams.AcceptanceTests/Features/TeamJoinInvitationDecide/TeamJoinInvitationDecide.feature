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

        Сценарий: Отказ приглашения в команду
            Когда 'Маша' отклоняет приглашение в команду 'FooBar'
            Тогда в команде 'FooBar' количество участников равно '1'