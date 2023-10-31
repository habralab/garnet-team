﻿Функция:
Я, как пользователь-участник проекта
Хочу иметь возможность редактировать название задачи внутри проекта
Чтобы пользователи могли точно понимать суть задачи

    Контекст:
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует проект 'FooBar'
        И существует команда 'DreamTeam'
        И команда 'DreamTeam' является участником проекта 'FooBar'
        И пользователь 'Вася' является участником команды 'DreamTeam'
        И существует задача 'DoSomething' в проекте 'FooBar'

    Сценарий: Редактирование задачи
        Когда пользователь 'Вася' редактирует название задачи 'DoSomething' на 'ToDo'
        Тогда в проекте 'FooBar' существует задача с названием 'ToDo'

    Сценарий: Редактирование задачи возможно только участником проекта
        Когда пользователь 'Маша' редактирует название задачи 'DoSomething' на 'ToDo'
        Тогда пользователь получает ошибку, что 'ProjectOnlyParticipantCanEditTaskError'
        И в проекте 'FooBar' существует задача с названием 'DoSomething'