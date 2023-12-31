﻿Функция:
Я, как пользователь-владелец команды
Хочу иметь возможность искать проекты по определенным фильтрам
Чтобы в конце концов найти интересующие меня проекты, в которых я готов поучаствовать

    Сценарий: Полнотекстовый поиск проектов по наименованию
        Допустим существует проект 'Dummy'
        И существует проект 'Yummy'
        Когда производится поиск проектов по запросу 'Yum'
        Тогда в списке отображается '1' проект

    Сценарий: Полнотекстовый поиск проектов по описанию
        Допустим существует проект 'Dummy' с описанием 'The best project in the world'
        И существует проект 'Yummy' с описанием 'My only wish is to be alive'
        Когда производится поиск проектов по запросу 'alive'
        Тогда в списке отображается '1' проект

    Сценарий: Поиск проектов по тегам
        Допустим существует проект 'Dummy' с тегами 'marketing, smm'
        И существует проект 'Yummy' с тегами 'building, manufacture'
        Когда производится поиск проектов по тегу 'smm'
        Тогда в списке отображается '1' проект