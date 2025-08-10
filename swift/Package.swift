// swift-tools-version: 5.9
// The swift-tools-version declares the minimum version of Swift required to build this package.

import PackageDescription

let package = Package(
    name: "CoffeeShopKata",
    products: [
        .library(
            name: "CoffeeShopKata",
            targets: ["CoffeeShopKata"]
        ),
    ],
    targets: [
        .target(name: "CoffeeShopKata"),
        .testTarget(
            name: "CoffeeShopKataTests",
            dependencies: ["CoffeeShopKata"]
        ),
    ]
)
