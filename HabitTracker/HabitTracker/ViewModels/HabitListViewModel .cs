using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitTracker.Data;
using HabitTracker.Models;
using HabitTracker.Services;
using System.Collections.ObjectModel;

namespace HabitTracker.ViewModels;

public partial class HabitListViewModel
{
    private readonly HabitService _habitService;

    [ObservableProperty]
    private ObservableCollection<Habit> habits = new();

    [ObservableProperty]
    private bool isLoading;

    public HabitListViewModel()
    {
        _habitService = new HabitService(new AppDbContext());
    }

    [RelayCommand]
    private async Task LoadHabits()
    {
        try
        {
            IsLoading = true;
            var habits = await _habitService.GetAllHabitsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Habits.Clear();
                foreach (var habit in habits)
                {
                    Habits.Add(habit);
                }
            });
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddHabit()
    {
        var habit = await _habitService.AddHabitAsync(
            "Новая привычка",
            "Описание",
            "#FF5733"
        );

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Habits.Insert(0, habit);
        });
    }

    [RelayCommand]
    private async Task DeleteHabit(int habitId)
    {
        await _habitService.DeleteHabitAsync(habitId);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            var habit = Habits.FirstOrDefault(h => h.Id == habitId);
            if (habit != null)
                Habits.Remove(habit);
        });
    }
}
