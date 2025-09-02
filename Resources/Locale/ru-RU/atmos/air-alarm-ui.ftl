# UI


## Window

air-alarm-ui-title = Air Alarm
air-alarm-ui-access-denied = Недостаточный уровень доступа!
air-alarm-ui-window-pressure-label = Давление
air-alarm-ui-window-temperature-label = Температура
air-alarm-ui-window-alarm-state-label = Статус
air-alarm-ui-window-address-label = Адрес
air-alarm-ui-window-device-count-label = Всего устройств
air-alarm-ui-window-resync-devices-label = Ресинхр
air-alarm-ui-window-mode-label = Режим
air-alarm-ui-window-mode-select-locked-label = [bold][color=red] Mode selector failure! [/color][/bold]
air-alarm-ui-window-auto-mode-label = Режим Авто
-air-alarm-state-name =
    { $state ->
        [normal] Normal
        [warning] Warning
        [danger] Danger
        [emagged] Emagged
       *[invalid] Invalid
    }
air-alarm-ui-window-listing-title = {$address} : {-air-alarm-state-name(state:$state)}
air-alarm-ui-window-pressure = { $pressure } кПа
air-alarm-ui-window-pressure-indicator = Давление: [color={ $color }]{ $pressure } кПа[/color]
air-alarm-ui-window-temperature = { $tempC } °C ({ $temperature } K)
air-alarm-ui-window-temperature-indicator = Температура: [color={ $color }]{ $tempC } C ({ $temperature } K)[/color]
air-alarm-ui-window-alarm-state = [color={ $color }]{ $state }[/color]
air-alarm-ui-window-alarm-state-indicator = Состояние: [color={ $color }]{ $state }[/color]
air-alarm-ui-window-tab-vents = Вентиляции
air-alarm-ui-window-tab-scrubbers = Скрубберы
air-alarm-ui-window-tab-sensors = Сенсоры
air-alarm-ui-gases = { $gas }: { $amount } моль ({ $percentage }%)
air-alarm-ui-gases-indicator = { $gas }: [color={ $color }]{ $amount } моль ({ $percentage }%)[/color]
air-alarm-ui-mode-filtering = Фильтрация
air-alarm-ui-mode-wide-filtering = Фильтрация (широкая)
air-alarm-ui-mode-fill = Заполнение
air-alarm-ui-mode-panic = Паника
air-alarm-ui-mode-none = Нет
air-alarm-ui-pump-direction-siphoning = Siphoning
air-alarm-ui-pump-direction-scrubbing = Scrubbing
air-alarm-ui-pump-direction-releasing = Releasing
air-alarm-ui-pressure-bound-nobound = No Bound
air-alarm-ui-pressure-bound-internalbound = Internal Bound
air-alarm-ui-pressure-bound-externalbound = External Bound
air-alarm-ui-pressure-bound-both = Both
air-alarm-ui-widget-gas-filters = Gas Filters

## Widgets


### General

air-alarm-ui-widget-enable = Включено
air-alarm-ui-widget-copy = Скопировать настройки на аналогичные устройства
air-alarm-ui-widget-copy-tooltip = Копирует настройки данного устройства на все устройства данной вкладки воздушной сигнализации.
air-alarm-ui-widget-ignore = Игнорировать
air-alarm-ui-atmos-net-device-label = Адрес: { $address }

### Vent pumps

air-alarm-ui-vent-pump-label = Направление вентиляции
air-alarm-ui-vent-pressure-label = Ограничение давления
air-alarm-ui-vent-external-bound-label = Внешняя граница
air-alarm-ui-vent-internal-bound-label = Внутренняя граница

### Scrubbers

air-alarm-ui-scrubber-pump-direction-label = Направление
air-alarm-ui-scrubber-volume-rate-label = Объём (Л)
air-alarm-ui-scrubber-wide-net-label = ШирокаяСеть
air-alarm-ui-scrubber-select-all-gases-label = Select all
air-alarm-ui-scrubber-deselect-all-gases-label = Deselect all

### Thresholds

air-alarm-ui-sensor-gases = Газы
air-alarm-ui-sensor-thresholds = Пороги
air-alarm-ui-thresholds-pressure-title = Пороги (кПа)
air-alarm-ui-thresholds-temperature-title = Пороги (K)
air-alarm-ui-thresholds-gas-title = Пороги (%)
air-alarm-ui-thresholds-upper-bound = Опасность выше
air-alarm-ui-thresholds-lower-bound = Опасность ниже
air-alarm-ui-thresholds-upper-warning-bound = Предупреждение выше
air-alarm-ui-thresholds-lower-warning-bound = Предупреждение ниже
air-alarm-ui-thresholds-copy = Скопировать пороговые значения на все устройства
air-alarm-ui-thresholds-copy-tooltip = Копирует пороговые значения сенсоров этого устройства на все устройства на этой вкладке воздушной сигнализации.
