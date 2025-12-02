namespace NetWatch.Model
{
    public enum AlertType
    {
        NewDevice,          // Обнаружено новое устройство в сети
        DeviceOffline,      // Устройство, которое было онлайн, стало офлайн
        DeviceOnline,       // Устройство, которое было офлайн, стало онлайн
        PortOpen,           // Обнаружен открытый порт
        UnauthorizedDevice, // Обнаружено неавторизованное устройство
        MacAddressChanged,  // У устройства изменился MAC-адрес
        IpConflict,         // Обнаружен конфликт IP-адресов
        HighLatency,        // Высокая задержка ответа от устройства
        UnknownDeviceType,  // Не удалось определить тип устройства
        ScanCompleted,      // Сканирование сети завершено
        ScanFailed,         // Ошибка при сканировании сети
        DeviceRemoved       // Устройство удалено из мониторинга
    }
}
