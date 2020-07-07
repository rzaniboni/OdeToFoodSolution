using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OdeToFood.Utils {
  public static class DbHealthCheckProvider {
    public static HealthCheckResult Check (string connectionString) {
      return HealthCheckResult.Unhealthy ();
    }
  }
}