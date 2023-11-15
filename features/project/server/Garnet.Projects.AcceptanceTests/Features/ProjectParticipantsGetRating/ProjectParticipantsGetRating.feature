﻿Функция:
Я, как пользователь-участник проекта
Хочу получать рейтинг за выполненную задачу
Чтобы другие пользователи могли оценить меня как специалиста по определенным навыкам

    Контекст:
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует пользователь 'Дима'
        И существует проект 'FooBar' с владельцем 'Дима'
        И существует команда 'DreamTeam'
        И команда 'DreamTeam' является участником проекта 'FooBar'
        И пользователь 'Вася' является участником команды 'DreamTeam'
        И пользователь 'Маша' является участником команды 'DreamTeam'
        И существует задача 'DoSomething' в проекте 'FooBar' с тегами 'C#, SQL'
        И пользователь 'Вася' является ответственным по задаче 'DoSomething'
        И пользователь 'Вася' является исполнителем задачи 'DoSomething'
        И пользователь 'Маша' является исполнителем задачи 'DoSomething'

    Сценарий: Закрытие задачи с расчетом рейтинга
        Когда пользователь 'Вася' закрывает задачу с названием 'DoSomething'
        Тогда в системе существует задача с названием 'DoSomething' и статусом 'Close'
        И у пользователя 'Вася' общий рейтинг равен '3,8' а рейтинг каждого из навыков 'C#, SQL' равен '1,9'
        И у команды 'DreamTeam' общий рейтинг равен '1,9'
        И у пользователя 'Дима' общий рейтинг равен '0,5'

    Сценарий: Повторное закрытие задачи без расчета рейтинга
        Допустим задача 'DoSomething' повторно открыта
        Когда пользователь 'Вася' закрывает задачу с названием 'DoSomething'
        Тогда в системе существует задача с названием 'DoSomething' и статусом 'Close'
        И у пользователя 'Вася' общий рейтинг равен '0' а рейтинг каждого из навыков 'C#, SQL' равен '0'
        И у пользователя 'Маша' общий рейтинг равен '0' а рейтинг каждого из навыков 'C#, SQL' равен '0'
        И у команды 'DreamTeam' общий рейтинг равен '0'
        И у пользователя 'Дима' общий рейтинг равен '0'