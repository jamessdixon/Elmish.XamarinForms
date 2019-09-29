﻿// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace Fabimals.Components

open Fabulous.XamarinForms
open Xamarin.Forms
open Fabimals.Models

module SearchHandlers =
    type Msg =
        | QueryChanged of string
        | AnimalSelected of Animal

    let animalSearchHandler animals dispatch =
        View.SearchHandler(
            placeholder="Enter search term",
            showsResults=true,
            queryChanged=(fun (_, newValue) -> dispatch (QueryChanged newValue)),
            itemSelected=(fun item ->
                let data = item :?> ItemListElementData
                let animal = data.Key.GetAttributeKeyed(ViewAttributes.TagAttribKey) :?> Animal
                dispatch (AnimalSelected animal)),
            items=[
                for animal in animals do
                    yield View.Grid(
                        tag=animal,
                        padding=Thickness.Uniform 10.,
                        coldefs=[ RowOrColumn.Stars 0.15; RowOrColumn.Stars 0.85],
                        children=[
                            View.Image(
                                source=Image.Path animal.ImageUrl,
                                aspect=Aspect.AspectFill,
                                height=40.,
                                width=40.
                            )
                            View.Label(
                                text=animal.Name,
                                fontAttributes=FontAttributes.Bold
                            ).GridColumn(1)
                        ]
                    )
            ]
        )