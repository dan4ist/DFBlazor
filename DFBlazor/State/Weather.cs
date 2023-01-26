using DFBlazor.Data;
using Fluxor;

namespace DFBlazor.State {
    public class FetchWeatherAction {
    }

    public class FetchDataResultAction {
        public IEnumerable<WeatherForecast> Forecasts { get; }
        public FetchDataResultAction(IEnumerable<WeatherForecast> forecasts) {
            Forecasts = forecasts;
        }
    }

    [FeatureState]
    public class WeatherState {
        public bool IsLoading { get; }
        public IEnumerable<WeatherForecast> Forecasts { get; }

        public WeatherState() { }
        public WeatherState(bool isLoading, IEnumerable<WeatherForecast> forecasts) {
            IsLoading = isLoading;
            Forecasts = forecasts;
        }

    }

    public static class Reducers {

        [ReducerMethod]
        public static WeatherState ReduceFetchDataAction(WeatherState state, FetchWeatherAction action) =>
            new WeatherState(isLoading: true, forecasts: null);

        [ReducerMethod]
        public static WeatherState ReduceFetchDataResultAction(WeatherState state, FetchDataResultAction action) =>
            new WeatherState(isLoading: false, forecasts: action.Forecasts);
    }

    public class Effects {

        //private readonly IHttpClientFactory _httpClientFactory;

        //public Effects(IHttpClientFactory httpClientFactory) {
        //    _httpClientFactory = httpClientFactory;
        //} 


        [EffectMethod]
        public async Task HandleFetchWeatherAction(FetchWeatherAction action, IDispatcher dispatcher) {

            WeatherForecastService forcastService = new WeatherForecastService();
            var forecasts = await forcastService.GetForecastAsync(DateTime.Now);
            //var client = _httpClientFactory.CreateClient();
            //var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            if (forecasts is not null) {
                dispatcher.Dispatch(new FetchDataResultAction(forecasts: forecasts!));
            }
        }
    }
}
