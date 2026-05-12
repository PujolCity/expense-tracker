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

    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"expenses/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ExpenseResponse>> GetAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<ExpenseResponse>>("expenses");

        return response ?? [];
    }

    public async Task<ExpenseResponse?> GetExpenseByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<ExpenseResponse>(
            $"expenses/{id}");
    }

    public async Task UpdateExpenseAsync(Guid id, UpdateExpenseRequest request)
    {
        var response = await _httpClient.PatchAsJsonAsync(
            $"expenses/{id}",
            request);

        response.EnsureSuccessStatusCode();
    }
}
