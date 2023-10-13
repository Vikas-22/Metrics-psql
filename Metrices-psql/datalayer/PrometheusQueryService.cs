namespace Metrices_psql.datalayer
{
    using Metrices_psql.Models;
    using Newtonsoft.Json.Linq;
    using Prometheus;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PrometheusQueryService :IPrometheusQueryServices
    {
        private readonly HttpClient _httpClient;

        public PrometheusQueryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("http://localhost:9090/api/v1/");
        }

        static Prometheus.Counter TotalIndexReached = Metrics.CreateCounter("Total_index_page_reached", "Total_request_got_for_index");


        static Gauge TotalEmployeesByDepartment = Metrics.CreateGauge("Total_Employee_in_System_ByDepartment", "Current_number_of_Employees_By_Deparment", new GaugeConfiguration
        {
            LabelNames = new[] { "department" }
        });

        //        static readonly Histogram EmployeeCreationDuration = Metrics.CreateHistogram(
        //    "employee_creation_duration_seconds",
        //    "Duration of employee creation operations in seconds",
        //    new HistogramConfiguration
        //    {
        //        Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.1, count: 10),
        //    }
        //);

        private static readonly Summary EmployeeFindDurationSummary = Metrics.CreateSummary(
              "employee_find_duration_seconds_summary",
              "Summary of employee find operation duration in seconds"
          );

     

        static Gauge TotalEmployeesOverall = Metrics.CreateGauge("Total_Employee_Overall", "Current_number_of_Employees_Overall");

      
        
        
        
        //---------------------------------------------------------------------------------------------------
        
        public void TotalEmployesIncBYDepartment(Employes employes)
        {
            if (employes.Department != null)
            {
                string departmetlabel = employes.Department.ToString();
                TotalEmployeesByDepartment.WithLabels(departmetlabel).Inc();
            }
           

        }

        public void TotalEmployesDecByDepartment(Employes employee)
        {
            if (employee.Department != null)
            {
                string departmentlabel = employee.Department.ToString();
                TotalEmployeesByDepartment.WithLabels(departmentlabel).Dec();
            }
        }

        public void TotalIndexInc()
        {

            TotalIndexReached.Inc();
     
        }

        public void TotalEmployesInSystemIncandDec(double value)
        {
            TotalEmployeesOverall.Inc(value);
        }

        public void RecordEmployeeFindDuration(TimeSpan duration)
        {
           
            EmployeeFindDurationSummary.Observe(duration.TotalSeconds);
        }

        //Sent data ------------------------------------------------------------------------------------------
        //public async Task<string> TotalEmployesCreatedPromethues()
        //{
        //    //querry to retrieve data for the past 1 hour
        //    var promQLQuery = "employee_create_total[1h]"; 

        //    // Building the url for the query.
        //    // var queryUrl = $"query_range?query={promQLQuery}&start={DateTime.UtcNow.AddMinutes(-60).ToString("yyyy-MM-ddTHH:mm:ssZ")}&end={DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")}&step=15s";
        //    var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";
        //    //http://localhost:9090/api/v1/query?query=Total_Employee_in_System[1h]
        //    // Send the query to Prometheus
        //    var response = await _httpClient.GetAsync(queryUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadAsStringAsync();
        //    }

        //    throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        //}



        //recieve data





        public async Task<string> TotalIndexReachedPromethues()
        {
            //querry to retrieve data for the past 1 hour
            var promQLQuery = "Total_index_page_reached[1h]";

            // Building the url for the query.
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            // Send the query to Prometheus
            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        public async Task<string> TotalEmployesOverallSystemPromethues()
        {
            //querry to retrieve data for the past 1 hour
            var promQLQuery = "Total_Employee_Overall[1h]";

            // Building the url for the query.
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            // Send the query to Prometheus
            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        public async Task<string> CustomQueryPromethues(string customquery)
        {
            
            var promQLQuery = customquery;

            // Building the url for the query.
            // var queryUrl = $"query_range?query={promQLQuery}&start={DateTime.UtcNow.AddMinutes(-60).ToString("yyyy-MM-ddTHH:mm:ssZ")}&end={DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")}&step=15s";
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";
            //http://localhost:9090/api/v1/query?query=Total_Employee_in_System[1h]
            // Send the query to Prometheus
            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        public async Task<string> TotalEmployesByDepartment()
        {
            //querry to retrieve data for the past 1 hour
            var promQLQuery = "Total_Employee_in_System_ByDepartment[1h]";

            // Building the url for the query.
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            // Send the query to Prometheus
            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        
       //ChartData ---------------------------------------------------------------------------------------------------
        public async Task<List<ChartDataPoint>> ChartDataTotalEmployeesByDepartment()
        {
            var promQLQuery = "Total_Employee_in_System_ByDepartment[1h]";
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse the JSON response from Prometheus
                var prometheusData = JObject.Parse(responseContent);

                // Extract the "result" data
                var results = prometheusData["data"]["result"];

                // Create a list of ChartDataPoint objects to store data points
                var chartData = new List<ChartDataPoint>();

                foreach (var result in results)
                {
                    var metric = result["metric"];
                    var department = metric["department"].ToString();
                    var values = result["values"];

                    foreach (var value in values)
                    {
                        // Declare variables inside the inner loop
                        var timestamp = Convert.ToDouble(value[0]);
                        var metricValue = Convert.ToDouble(value[1]);

                        // Convert the Unix timestamp to IST
                        var istTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).ToOffset(TimeSpan.FromHours(5.5)); // IST offset is UTC+5:30

                        // Create a ChartDataPoint object and add it to the list
                        chartData.Add(new ChartDataPoint
                        {
                            Department = department,
                            Timestamp = istTime.UtcDateTime,
                            Value = metricValue
                        });
                    }
                }

                return chartData;
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        public async Task<List<ChartDataCounters>> ChartDataTotalEmployes()
        {
            var promQLQuery = "Total_Employee_Overall[1h]";
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse the JSON response from Prometheus
                var prometheusData = JObject.Parse(responseContent);

                // Extract the "result" data
                var results = prometheusData["data"]["result"];

                // Create a list of ChartDataPoint objects to store data points
                var chartData = new List<ChartDataCounters>();

                foreach (var result in results)
                {
                    var metric = result["metric"];
                    var values = result["values"];

                    foreach (var value in values)
                    {
                        // Declare variables inside the inner loop
                        var timestamp = Convert.ToDouble(value[0]);
                        var metricValue = Convert.ToDouble(value[1]);

                        // Convert the Unix timestamp to IST
                        var istTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).ToOffset(TimeSpan.FromHours(5.5)); // IST offset is UTC+5:30

                        // Create a ChartDataPoint object and add it to the list
                        chartData.Add(new ChartDataCounters
                        {
                           
                            Timestamp = istTime.UtcDateTime,
                            Value = metricValue
                        });
                    }
                }

                return chartData;
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }

        public async Task<List<ChartDataCounters>> ChartDataIndexCount()
        {
            var promQLQuery = "Total_index_page_reached[1h]";
            var queryUrl = $"http://localhost:9090/api/v1/query?query={promQLQuery}";

            var response = await _httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse the JSON response from Prometheus
                var prometheusData = JObject.Parse(responseContent);

                // Extract the "result" data
                var results = prometheusData["data"]["result"];

                // Create a list of ChartDataPoint objects to store data points
                var chartData = new List<ChartDataCounters>();

                foreach (var result in results)
                {
                    var metric = result["metric"];
                    var values = result["values"];

                    foreach (var value in values)
                    {
                        // Declare variables inside the inner loop
                        var timestamp = Convert.ToDouble(value[0]);
                        var metricValue = Convert.ToDouble(value[1]);

                        // Convert the Unix timestamp to IST
                        var istTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).ToOffset(TimeSpan.FromHours(5.5)); // IST offset is UTC+5:30

                        // Create a ChartDataPoint object and add it to the list
                        chartData.Add(new ChartDataCounters
                        {

                            Timestamp = istTime.UtcDateTime,
                            Value = metricValue
                        });
                    }
                }

                return chartData;
            }

            throw new Exception($"Failed to query Prometheus: {response.ReasonPhrase}");
        }
    }

}



