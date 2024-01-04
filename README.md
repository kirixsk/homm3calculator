This is a fork of https://github.com/zomle/homm3calculator

# HOMM3 Hota calculator

Purpose of the app is to help with guard calculations in Heroes of Might and Magic 3 Horn of the Abyss random templates. 
The calculations are based solely on user input, hard coded AI values and well-known formulas from The Heroes 3 wiki (no game memory access or any kind of *cheating* is used):
* [Template editor](https://heroes.thelazy.net//index.php/Template_Editor#About_Objects_and_Guards)
* [List of creatures (HotA)](https://heroes.thelazy.net/index.php/List_of_creatures_(HotA))

### How to use
Go to https://homm3webapp.azurewebsites.net/

After starting the program:
* Select *Monster Strength* used when setting up the random template before the game started.
* Enter the current *Week*.
* Select the *Monster Strength in Zone* of the guard (usually represented by the number of swords on the zone on template images).
* If a ZoneGuard was selected, enter the *Zone Guard Value* (often 3000, 6000, 9000, 12500, 45000, etc.), in this case the Map Objects are irrelevant.
* Otherwise select the *Map Objects* that are guarded.
* If a creature dwelling was selected, the total *Number of zones with towns* has to be entered, and *Number of xy zones*, where xy is the same faction as the creature dwelling. This is required, because the AI value of dwellings depends on these values. If the number of specific faction zones are unknown, then guess...
* Select the guard.
* The estimated number of guard will appear.

### Development environment

* I used *Visual Studio 2022 Community Edition*

## Authors

* **zomle** - *Initial work* - https://github.com/zomle
* **kirixsk** - *Web app* - https://github.com/kirixsk

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Thanks for all the infor on the [Heroes 3 wiki](https://heroes.thelazy.net/). 
