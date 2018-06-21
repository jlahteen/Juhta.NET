﻿# Main Namespaces of Juhta.NET

------------------------------

## Juhta.Net

Juhta.Net is the root namespace of the framework. It contains global-scope classes that provide easy access to the most common services such as logging and the application itself, if one has been started.

At the process level, Juhta.NET supports an application model. This means that you can configure and start a logical application, which is a singleton instance of the Application class. You can also configure any number of libraries to start with the application. These libraries are managed by Juhta.NET Library Manager.

An application can be of any type, from console applications to web applications and web API services.

## Juhta.Net.Common

Juhta.Net.Common is the home for all useful common classes that have no special relationship to any separate feature or capability that Juhta.NET provides.

## Juhta.Net.Console

Juhta.Net.Console contains classes that help building console applications. At this moment, the namespace provides a versatile support for command line argument parsing, which is a quite typical challenge for console applications.

## Juhta.Net.Diagnostics

There is always room for good logging and tracing capabilities, and that is exactly what the Juhta.Net.Diagnostics namespace aims to provide. For the moment, the core Juhta.Net library provides a generic logging model with a built-in file logger, but in the roadmap, there is a separate Juhta.Net.Diagnostics library, which will extend logging and tracing features to a whole new level.

## Juhta.Net.Extensions

Juhta.Net.Extensions is the namespace for extension classes. These extension classes contain nothing but extension methods. Furthermore, all extension methods have been divided into separate classes according to the corresponding classes they extend.

## Juhta.Net.Helpers

The namespace Juhta.Net.Helpers is reserved for so-called helper classes. Helper classes have no official definition, but their purpose is to facilitate certain operations in a quite restricted context. For now, in the namespace Juhta.Net.Helpers there is only one helper class defined that facilitates argument validation.

## Juhta.Net.LibraryManagement

Juhta.Net.LibraryManagement provides comprehensive library management functionalities. With the Juhta.NET library management, you can start and close your application gracefully by just implementing appropriate library management interfaces for your libraries. Juhta.NET takes care of all startup and shutdown code for you.

The library management supports also so-called dynamic libraries, which means you can change the configuration of your libraries on the fly. Of course, to make that possible, your libraries have to implement appropriate dynamic library interfaces.

## Juhta.Net.Processing

Juhta.Net.Processing is for now just a one-class namespace, but in the future, this namespace is planned to provide classes for implementing background processes for different kind of processing scenarios. These scenarios may vary from a single-threaded timed interval processes to multi-threaded request/response based processes.

## Juhta.Net.Services

Juhta.Net.Services provides dependency injection services for the application. These dependency injection services can comprise of any number of configured services. Each dependency injection service can also have any number of configured constructor parameters with values. Dependency injection services can even have other dependency injection services as constructor parameters. This makes it possible to define aggregate services.

The running application doesn’t have to know anything about how services are being constructed. An application just creates instances of services by corresponding service types or service identifiers.

## Juhta.Net.Validators

Juhta.Net.Validators defines common data validation interfaces and provides a set of validation classes. The goal of this namespace is to introduce a uniform model for validating any kind of data from scalar values to aggregate objects. The other important goal is to provide ready validation classes for the most common business data validation needs.
