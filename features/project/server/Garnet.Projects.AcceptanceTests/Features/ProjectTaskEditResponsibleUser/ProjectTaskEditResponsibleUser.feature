﻿Функция:
Я, как пользователь-участник проекта
Хочу иметь возможность редактировать ответственного задачи внутри проекта
Чтобы участники проекта знали кто проверяет выполнение задачи

    Контекст:
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Дима'
        И существует пользователь 'Маша'
        И существует проект 'FooBar'
        И пользователь 'Вася' является владельцем проекта 'FooBar'
        И существует команда 'DreamTeam'
        И команда 'DreamTeam' является участником проекта 'FooBar'
        И пользователь 'Маша' является участником команды 'DreamTeam'
        И существует задача 'DoSomething' в проекте 'FooBar'
        И пользователь 'Дима' является отвественным по задаче 'DoSomething'

    Сценарий: Изменение отвественного по задаче владельцем проекта
        Когда пользователь 'Вася' меняет отвественного по задаче 'DoSomething' на пользователя 'Маша'
        Тогда в системе существует задача с названием 'DoSomething'
        И отвественным по задаче 'DoSomething' является пользователь 'Маша'

    Сценарий: Изменение отвественного по задаче самим отвественным
        Когда пользователь 'Дима' меняет отвественного по задаче 'DoSomething' на пользователя 'Маша'
        Тогда в системе существует задача с названием 'DoSomething'
        И отвественным по задаче 'DoSomething' является пользователь 'Маша'

    Сценарий: Изменение отвественного возможно только владельцем проекта или самим отвественным по задаче
        Когда пользователь 'Маша' меняет отвественного по задаче 'DoSomething' на пользователя 'Маша'
        Тогда пользователь получает ошибку, что 'ProjectTaskResponsiblePersonOnlyCanEditResponsibleUserError'
        И в системе существует задача с названием 'DoSomething'
        И отвественным по задаче 'DoSomething' является пользователь 'Дима'