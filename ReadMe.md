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
- Xamarin Community Toolkit
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

M1 is all about laying the groundwork. Don't expect anything fancy here. At this phase the application is barely usable.

- [ ] Static Grid
- [x] 4 columns
- [x] Show *something* on the tiles (text/image/icon/etc.)
- [x] Handle click event on tiles
- [x] Pin installed apps to the screen
- [x] Unpin installed apps from the main screen
- [x] Launch pinned apps
- [x] Uninstall app
- [ ] Data structure to store tiles on the main screen
- [ ] Persist tile arrangement
- [x] Global settings service with fixed options
- [ ] Persist settings

### M2

M2 is the beginning to make the launcher usable in everyday usecases.

- [ ] Start working on "launcher apps/widgets/live tiles"
    - [ ] Clock
    - [ ] Weather
    - [ ] Me/Timeline
    - [ ] Launcher settings
    - [ ] Calendar
    - [ ] Mail
    - [ ] Messages
    - [ ] Photos
    - [ ] Contacts
- [ ] Rearrange tiles on the grid - (quadtree?)
- [ ] Resize tiles / Multi size app tiles
- [ ] Feedback to the user when clicked (Rotation/animations/etc)
- [x] Change accent color
- [ ] Make repository public

### M3

M3 is the preparation for a first public beta release.

- [ ] Stack layout mode
- [ ] Further development TBD
- [ ] More tiles
- [ ] Optimizations
- [ ] Bug fixes
- [ ] Multi lang support
- [ ] Light mode/Dark mode support
- [ ] First public beta version (?)

### M4

- [ ] Port to MAUI
- [ ] Landscape mode support
- [ ] Background image with transparent tiles
- [ ] 6 column mode setting
- [ ] WP7/7.8 start screen setting

### Current progress

Work is underway on M1 and some low hanging fruits from M2.

## Tiles

### Clock
### Weather
### Me/Timeline
### Launcher settings
### Calendar
### Mail
### Messages



## Development log

### 2022/04/07

- Select tile to rearrange
- Unselect tile when tapping on it the second time
- Unselect tile when tapping on the background

### 2021/11/12

- Invalidate icon cache

### 2021/11/09

- Framework for "integrated launcher applications"
- Launcher settings page
- Change accent color from launcher settings

### 2021/10/30

- Use carouselPage instead of tabbedPage

### 2021/10/27

- Show application icons on pinned tiles
- Mitigated a Xamarin bug by writing icon files to the internal storage then reading them back from the storage by using `ImageSource.FromFile` instead of `ImageSource.FromStream()` Its also needed to call `ForceLayout()` on the scrollview and `Layout()` on the grid component
- Updated to the latest Xamarin release

### 2021/10/18

- Uninstall applications
- Unpin uninstalled apps from the start screen
- Crude caching mechanism for the application list
- Listen to application uninstall event on android

### 2021/10/16

- Now it is possible to pin tiles to the start screen
- Now it is possible to unpin tiles from the start screen
- Fixed a sizing issue with the start screen

###  2021/10/09

- Reimplemented grid using the MVVM pattern with datatemplates.

### 2021/10/04

- Moved application list functionality from code-behind to ViewModel

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