# WPLauncher

## Features

- WP7/8 style live tiles
- App list
- Pin apps to the screen
- "System" and Custom apps
- Resizable tiles
- 4 column mode
- Launch installed apps

## Tech stack
- Xamarin Forms
- C#
- Autofac
- API Level 28

## Planned live custom tiles / "widgets"

Widgets with live data

- Me (Timeline / notifications / tasks / slideshows / weather info / travel info / clock / twitter / fb / etc.)
- Photos slideshow
- Messages
- Weather
- Clock / Time
- News feed
- Tasks
- Calendar
- Email
- Hungarian name days

*Information gather via public API-s. When public API-s are not available extract information from system notifications if its possible* 

## Milestones

### M1

- [ ] Static Grid
- [x] 4 columns
- [x] Show *something* on the tiles (text/image/icon/etc.)
- [ ] Handle click event on tiles
- [ ] Pin installed apps to the screen
- [ ] Unpin installed apps from the main screen
- [ ] Launch pinned apps
- [ ] Data structure to store tiles on the main screen

### M2

- [ ] Start working on "launcher apps/widgets/live tiles"
    - [ ] Clock
    - [ ] Weather
    - [ ] Me/Timeline
    - [ ] Launcher settings
    - [ ] Calendar
    - [ ] Mail
    - [ ] Messages
- [ ] Rearrange tiles on the grid - (quadtree?)
- [ ] Resize tiles / Multi size app tiles
- [ ] Feedback to the user when clicked (Rotation/animations/etc)

### M3 

- [ ] Stack layout mode
- [ ] Further development TBD
- [ ] More tiles
- [ ] Optimizations
- [ ] Bug fixes

### Current progress

Work is underway on M1

## Tiles

### Clock
### Weather
### Me/Timeline
### Launcher settings
### Calendar
### Mail
### Messages



## Development log

### 2021/10/02

- Handle longpress event on application list

### 2021/09/29

- Fixed image and text positioning on tiles
- Fixed application permissions

### 2021/09/28

- Tiles are no longer empty squares

### 2021/09/20

- All tile sizes are accounted for
- Works with all of the possible arrangements

### 2021/09/19

- Initial commit
- Fixed 2 columns grid
- Square and Wide tiles