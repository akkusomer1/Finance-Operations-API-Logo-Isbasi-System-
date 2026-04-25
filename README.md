# Finance & Operations API

> A modular RESTful API backend for managing commercial finance and operations — inspired by SME accounting software like **Logo İşbaşı**.

A production-oriented backend that centralizes customer/supplier management, inventory tracking, order & invoice workflows, financial balance, and cash/bank operations under a single versioned API.

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 7) |
| ORM | Entity Framework Core + SQL Server |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Mediator | MediatR (Command/Query) |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| Documentation | Swagger / OpenAPI |
| Mail | MailKit |

## Architecture

Built on a layered structure following Clean Architecture principles.

```
ZeusApp
├── ZeusApp.WebApi          # Controllers, middleware, swagger, versioning
├── ZeusApp.Application     # Command/Query handlers, DTOs, validators
├── ZeusApp.Domain          # Domain entities and enums
├── ZeusApp.Infrastructure  # EF Core, repository, unit of work, seed
├── ZeusApp.Identity        # ASP.NET Core Identity, separate IdentityContext
└── ZeusApp.Shared          # Cross-cutting services (mail, date helpers)
```

## Features

### Customer / Supplier Management
- Individual and corporate type distinction
- Contact info, bank accounts, additional addresses and related persons
- Current account balance tracking based on transaction type, payment status and currency

### Product / Service Catalog
- Brand, category, service group and unit relationships
- Stock-linked product model with conditional inventory updates based on product/service type

### Invoice Processing (Purchase / Sales)
- VAT inclusive / exclusive calculation
- Rate or amount based discount support
- Multi-currency scenarios
- Delivery address variations
- Automatic stock and balance mutation on invoice creation

### Order Management
- Order status and carrier company tracking
- Paid / unpaid scenarios with remaining amount calculation
- Order-to-invoice conversion flow

### Finance & Operations Modules
- Cash register and bank (including general bank) management
- Expense and credit transactions
- Warehouse management
- Category and carrier company modules

### Identity & Authorization
- JWT Bearer token generation
- Role and claim based authorization
- Register, login, password reset flows
- Automatic role/claim seed mechanism
- Mail-based identity notifications

## Technical Highlights

### Financial Calculation Engine
Invoice and order operations go beyond simple CRUD. Within the same transaction flow: VAT, discounts, exchange rates, retail/wholesale scenarios, stock movements, and current account balance effects are all computed together — keeping operational and financial data consistently in sync.

### Data Management
- **Soft Delete** — Deleted records are automatically filtered application-wide via global query filters
- **Audit Trail** — Created/updated user and timestamp are automatically tracked on every entity
- **Unit of Work + Repository** — Transaction integrity managed centrally
- **Pagination & Projection** — Unnecessary data transfer is eliminated on list endpoints

### API Features
- Versioned API endpoints
- Swagger / OpenAPI documentation
- Centralized error handling with standard response structure
- Layered input validation via FluentValidation

## Getting Started

### Prerequisites
- .NET 7 SDK
- SQL Server (LocalDB or full instance)

### Setup
