# Cryptozon

## Software Requirements

* Visual Studio 2019
* .NET Core 2.2
* Postman  

## Folder Structure  

* api     - contains the WebApi code
* docs    - contains documentation
* postman - contains postman smoke tests  

## Assumptions

* Users/shoppers can purchase multiple cryptos at a time.
* Users/shoppers can purchase multiple or part of a crypto.
* There is enough stock of crypto coins for purchases.
* Purchase currency will be USD.
* Payment will be handled after the purchase is actioned by user and confirmed by the system, based on experience with online stores.
* All that is required for payment is the order reference and the total amount.  

## Goal  

The goal of the assignment is to give us insight on how confident you are with the c# stack in a e-commerce context

### Description/Requirements  

This assignment is to build the backend of a simple web commerce cart by using crypto currencies as product.  
Create an API that allow a user to buy crypto currencies from a list.  
The crypto currency data is pulled from https://coinmarketcap.com/api/ .

The api endpoints are:

* `GET: /products`
* `POST: /purchase`  

All purchases are persisted with a price information, that refers to the price of the cryptocurrency at the moment of the purchase and the id of the user who made the purchase.

At least 80% of the code must be covered with unit tests and integration tests
If you feel you can do more, here some __optional__ requirements:

* endpoints are authenticated
* function programming style, by using, for instance https://github.com/louthy/language-ext
* use docker to run the project

### Delivery  

* the project must be easily started by using Rider/Visual Studio Code/Visual Studio
* .NET Core version: latest, c# version: latest.
* the project must be pushed in a github/gitlab/bitbucket repository

### Time  

The execution time is expected to be 4-6 hours  
