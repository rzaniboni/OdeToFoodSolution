using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OdeToFood.Utils {
  public static class MqHealthCheckProvider {

    public static HealthCheckResult Check (string mqUri) {
      return HealthCheckResult.Healthy ();
    }

  }
}