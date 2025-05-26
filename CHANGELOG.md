# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

# [1.0.16] - 2025-05-26
### Fixed
- Fixed timestamp
### Removed
- OrderId check

# [1.0.15] - 2024-11-06
### Fixed
- Bump RestSharp from 109.0.1 to 112.0.0
- Updated tests
- Fixed refund requests

# [1.0.14] - 2024-08-29
### Added
- Added decimal VAT rate support

# [1.0.13] - 2024-06-18
### Fixed
- Fixed ci/cd

# [1.0.12] - 2024-06-18
### Fixed
- Fixed ci/cd

# [1.0.11] - 2024-06-18
### Fixed
- Fixed ci/cd

# [1.0.10] - 2024-06-14
### Added
- Added ci/cd

# [1.0.9] - 2024-06-05
### Fixed
- Fixed shop-in-shop validation

# [1.0.8] - 2023-11-16
### Added
- Added get a list of payment providers
- Added returns an array of grouped payment providers fields
- Added create email refund
- Added request card token
- Added request settlements
- Added request payment report
- Added create MiT payment
- Added create MiT authorization hold
- Added create CiT payment
- Added create CiT authorization hold
- Added commit MiT authorization hold
- Added commit CiT authorization hold
- Added revert existing Mit or CiT authorization hold
- Added request payment report by settlement ID
- Added save card details

# [1.0.7] - 2023-05-17
### Added
- Added support for Pay and Tokenize feature
### Fixed
- Fixed enum PaymentMethodGroup
- Code quality improvements and other fixes

# [1.0.5] - 2023-04-17
### Added
- Added callback handling feature
- Added  callbackUrl fields to PaymentRequest model
### Fixed
- Refactored fields follow C# convention

# [1.0.4] - 2023-04-13
### Added
- Added support for .NET standard 2.0 and 2.1

# [1.0.3] - 2023-04-12
### Added
- Added document for .NET SDK

# [1.0.2] - 2023-04-10
### Added
- Added config to install SDK via .NET Core CLI
- Added config to install SDK via Package Manager Console
### Fixed
- Code quality improvements


# [1.0.1] - 2023-04-04
### Added
- Added config to install SDK via Nuget Package Management
- Added create payment feature
- Added get payment feature
- Added create refund request feature
### Fixed
- Code quality improvements
