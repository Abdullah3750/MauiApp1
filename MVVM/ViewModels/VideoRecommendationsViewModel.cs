using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.MVVM.ViewModels
{
    public class VideoItem
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
    }

    public class VideoRecommendationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VideoItem> Videos { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly HttpClient _httpClient;

        public VideoRecommendationsViewModel()
        {
            _httpClient = new HttpClient();
        }

        public async Task LoadVideosAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return;

                Videos.Clear();

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://youtube138.p.rapidapi.com/search/?q={query}&hl=en&gl=US"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "b8c52ae982msh1b85acab6baa070p17084bjsnf0c43aa0d404" },
                        { "X-RapidAPI-Host", "youtube138.p.rapidapi.com" },
                    },
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var doc = await JsonDocument.ParseAsync(stream);

                    var root = doc.RootElement;

                    // Check if "contents" exists
                    if (root.TryGetProperty("contents", out JsonElement contents))
                    {
                        foreach (var item in contents.EnumerateArray())
                        {
                            // Check if the "video" property exists
                            if (item.TryGetProperty("video", out JsonElement video))
                            {
                                // Extract the videoId
                                if (!video.TryGetProperty("videoId", out JsonElement idElement))
                                    continue;

                                var id = idElement.GetString();
                                string title = "Unknown Title";

                                // Safely handle the "title" property whether it's an object or a string
                                if (video.TryGetProperty("title", out JsonElement titleElement))
                                {
                                    // Check if "title" is an object with runs
                                    if (titleElement.ValueKind == JsonValueKind.Object)
                                    {
                                        if (titleElement.TryGetProperty("runs", out JsonElement runs) &&
                                            runs.ValueKind == JsonValueKind.Array &&
                                            runs.GetArrayLength() > 0 &&
                                            runs[0].TryGetProperty("text", out JsonElement textElement))
                                        {
                                            title = textElement.GetString() ?? "Unknown Title";
                                        }
                                    }
                                    // Or if "title" is a simple string
                                    else if (titleElement.ValueKind == JsonValueKind.String)
                                    {
                                        title = titleElement.GetString() ?? "Unknown Title";
                                    }
                                }

                                // Build thumbnail URL
                                string thumbnail = $"https://i.ytimg.com/vi/{id}/hqdefault.jpg";

                                // Add to the collection
                                Videos.Add(new VideoItem
                                {
                                    Title = title,
                                    Thumbnail = thumbnail,
                                    Url = $"https://www.youtube.com/watch?v={id}"
                                });
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("No Results", "No video results found.", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"API call failed: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
            }
        }
    }
}
