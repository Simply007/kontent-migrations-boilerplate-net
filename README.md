# Kontent Migrations Boilerplate

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](code_of_conduct.md)

This is a simplified alternative [Kontent CLI migrations examples
](https://github.com/Kentico/kontent-migrations-boilerplate). It uses [Kontent Management Management API SDK](https://github.com/Kentico/kontent-management-sdk-net) to showcase simple migrations using mentioned SDK.

This repo is meant to be used as a boilerplate for more complex setup, that might i.e. record state of alreasy processed migrations, extends migration modules with other context data, or implements more robust retry policy and respects [rate limitations](https://docs.kontent.ai/reference/management-api-v2).

## Get started

* Copy `.environments.json.template` and name it `.environments.json`
* Fill the `projectId` and `apiKey`

> Alternatively, you can install Kontent CLI an run:
>
> `kontent environment add --name TARGET --project-id "<YOUR_PROJECT_GUID>" --api-key "<YOUR_MANAGAMENT_API_KEY>"`

* build the project by `dotnet build`
* run sample migrations `dotnet run`

## Sample migrations

Sample migrations create a bunch of blog posts with test field `Author`. Then transform this text field with full name into a separate content items Author and link these from the blog posts to reuse content. Ultimately removed old `Author` text field.
