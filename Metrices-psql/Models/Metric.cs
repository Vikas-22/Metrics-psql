namespace Metrices_psql.Models
{
    public class Metric
    {
        public int Id { get; set; }
        public string MetricName { get; set; }
        public int MetricValue { get; set; }
        public DateTime MetricTimestamp { get; set; } = DateTime.Now.ToUniversalTime();

    }
}
