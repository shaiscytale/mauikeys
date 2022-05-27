using mauikeys.Services;
namespace mauikeys.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new();
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey MAUI test app";
        this.monkeyService = monkeyService;
    }

    [ICommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();

            if (monkeys.Any())
                Monkeys.Clear();

            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            // create a fckn AlertService or whatever, this is eww
            await Shell.Current.DisplayAlert("Error", $"unable to get monkeys : {e.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
