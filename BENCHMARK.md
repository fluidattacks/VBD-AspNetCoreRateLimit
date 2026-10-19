# VBD-AspNetCoreRateLimit: synthetic vulnerability benchmark

This fork is a **synthetic, openly labelled benchmark**. It is not the upstream project
and must not be used as one. Each sample is an isolated pair of branches off `baseline`:

- `baseline`: the pinned upstream commit plus any hardening; the common ancestor.
- `smpl/<NNN>/safe`: the genuine negative (validated control) of sample `NNN`.
- `smpl/<NNN>/vuln`: `safe` plus exactly one commit that weakens the control.

The class and the coordinates of every sample live only in `GROUND_TRUTH.csv` on this
orphan branch, never in the analysed branches.

| sample | file | class |
|---|---|---|
| 001 | `src/AspNetCoreRateLimit/Middleware/RateLimitMiddleware.cs` | F115 Security controls bypass or absence (rate limiting; absent_control / debug backdoor) |
| 002 | `src/AspNetCoreRateLimit/Middleware/RateLimitConfiguration.cs` | F115 Security controls bypass or absence (rate_limit_key_trust / trusting a less-trusted source) |
| 003 | `src/AspNetCoreRateLimit/Core/RateLimitProcessor.cs` | F115 Security controls bypass or absence (client_trusted_value) |
