﻿Функция:
Я, как пользователь-участник проекта
Хочу иметь возможность редактировать лейблы задачи внутри проекта
Чтобы пользователям было легче найти нужную задачу

    Контекст:
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует проект 'FooBar'
        И существует команда 'DreamTeam'
        И команда 'DreamTeam' является участником проекта 'FooBar'
        И пользователь 'Вася' является участником команды 'DreamTeam'
        И существует задача 'DoSomething' в проекте 'FooBar'
        И лейблы задачи 'DoSomething' состоят из 'маркетинг, дизайн'

    Сценарий: Редактирование задачи
        Когда пользователь 'Вася' редактирует лейблы задачи с названием 'DoSomething' на 'бекэнд, чистая архитектура'
        Тогда в системе существует задача с названием 'DoSomething'
        И и лейблы задачи 'DoSomething' состоят из 'бекэнд, чистая архитектура'

    Сценарий: Редактирование задачи возможно только участником проекта
        Когда пользователь 'Маша' редактирует лейблы задачи с названием 'DoSomething' на 'фронтэнд, типизация'
        Тогда пользователь получает ошибку, что 'ProjectOnlyParticipantCanEditTaskError'
        И в системе существует задача с названием 'DoSomething'
        И и лейблы задачи 'DoSomething' состоят из 'маркетинг, дизайн'