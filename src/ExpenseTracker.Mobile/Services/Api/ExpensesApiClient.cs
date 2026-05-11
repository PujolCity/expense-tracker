using System.Net.Http.Json;
using ExpenseTracker.Mobile.Models.Expenses;

namespace ExpenseTracker.Mobile.Services.Api;

public class ExpensesApiClient : IExpensesApiClient
{
    private readonly HttpClient _httpClient;

    public ExpensesApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateExpenseAsync(CreateExpenseRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("expenses", request);

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ExpenseResponse>> GetAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<ExpenseResponse>>("expenses");

        return response ?? [];
    }


}
