using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OdeToFood.Utils {
  public class FilePathHealthCheck : IHealthCheck {
    private readonly string filePath;
    public FilePathHealthCheck (string filePath) {
      this.filePath = filePath;
    }

    public Task<HealthCheckResult> CheckHealthAsync (HealthCheckContext context, CancellationToken cancellationToken = default) {
      try {
        var testFile = $"{filePath}\\test.txt";
        var fs = File.Create (testFile);
        fs.Close ();
        File.Delete (testFile);

        return Task.FromResult (HealthCheckResult.Healthy ());

      } catch (Exception ex) {

        switch (context.Registration.FailureStatus) {
          case HealthStatus.Degraded:
            return Task.FromResult (HealthCheckResult.Degraded($"Issues writing to tile path", ex));
        }

        return Task.FromResult (HealthCheckResult.Unhealthy ());
      }
    }
  }
}