# BlastForce
### Concept
“Blast Force” is a fast-paced 3D platformer, using rocket jumping mechanics to explore and traverse various areas, grabbing the key object, then racing back to the start against a timer. Key mechanics is 360 degree firing of rockets while platforming to get through enemies, obstacles, or fire yourself to go faster or platform The story is about a walking tank named Sherman and his treasure hunting adventures to find the legendary Chekov’s Firing Pin.

### Project Schedule

<table>
    <thead>
        <tr>
            <th align="left"></th>
            <th colspan=3>January</th>
            <th colspan=4>February</th>
            <th colspan=4>March</th>
            <th colspan=4>April</th>
            <th colspan=2>May</th>
        </tr>
    </thead>
    <thead>
        <tr>
            <th align="left">Objective/Week</th>
            <th>1</th>
            <th>2</th>
            <th>3</th>
            <th>4</th>
            <th>5</th>
            <th>6</th>
            <th>7</th>
            <th>8</th>
            <th>9</th>
            <th>10</th>
            <th>11</th>
            <th>12</th>
            <th>13</th>
            <th>14</th>
            <th>15</th>
            <th>16</th>
            <th>17</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th align="left">Concept</th>
            <td>#</td>
            <td>#</td>
            <td>#</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <th align="left">Requirements and Design</th>
            <td></td>
            <td></td>
            <td></td>
            <td>#</td>
            <td>#</td>
            <td>#</td>
            <td>#</td>
            <td>#</td>
            <td>#</td>
            <td>##</td>
            <td>##</td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <th align="left">Implementation</th>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>#</td>
            <td>#</td>
            <td>##</td>
            <td>##</td>
            <td>##</td>
            <td>##</td>
            <td>##</td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <th align="left">Testing</th>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>##</td>
            <td>##</td>
            <td>##</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <th align="left">Integration - Showcase</th>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>##</td>
            <td>##</td>
        </tr>
    </tbody>
</table>
<table width=100%>
    <thead>
        <tr>
            <th colspan=4 align="center">Team Members Task Assignments</th>
        </tr>
    </thead>
    <tbody align="left">
        <tr>
            <th colspan=2>Team Lead:</th>
            <th colspan=2>Todd St. Onge</th>
        </tr>
        <tr>
            <th colspan=2>Github Manager:</th>
            <th colspan=2>Abdul Rahman Al-Sabri</th>
        </tr>
        <tr>
            <th rowspan=4>Design:</th>
            <td>Level Design:</td>
            <td>Jinyang Dong</td>
            <td>Abdul Rahman Al-Sabri</td>
        </tr>
        <tr>
            <td>3D Models Design:</td>
            <td colspan=2>Todd St. Onge</td>
        </tr>
        <tr>
            <td>3D Models Animation:</td>
            <td colspan=2>Homero Garza</td>
        </tr>
        <tr>
            <td>3D Models Textures:</td>
            <td colspan=2>Jordan Arevalos</td>
        </tr>
        <tr>
            <th rowspan=3>Implementation:</th>
            <td>Player/Level/Environment Programmer:</td>
            <td>Abdul Rahman Al-Sabri</td>
            <td>Alsalt Al-Fahdi</td>
        </tr>
        <tr>
            <td>Sound</td>
            <td colspan=2>Alsalt Al-Fahdi</td>
        </tr>
    </tbody>
</table>

### Cloning and Updating Instructions for contributers
##### To clone the repository for the first time, use the following command:
```
$ git clone https://github.com/AsaltAlfahdi/BlastForce.git
```
#
##### To get the most updated version of a previously cloned repository, use the following commands:
```
$ cd /path/of/repo
$ git fetch origin
$ git reset --hard origin/master
```
#
##### To view differences/changes of local cloned repository and actual master repository, use the following command:
```
$ git status
```
Result of command:
```
Changes not staged for commit:
  (use "git add/rm <file>..." to update what will be committed)
  (use "git restore <file>..." to discard changes in working directory)
        deleted:    .DS_Store
        modified:   Blender_Files_Assets/Sherman.blend
        modified:   README.md

no changes added to commit (use "git add" and/or "git commit -a")
```
#
##### To "commit" changes of a cloned repository,  use the following commands:
```
$ git add .
$ git commit -m "Message/Description of updated files"
```
Result of commands:
```
[master 0d5dcc2] Updated
 2 files changed, 2 insertions(+)
 delete mode 100644 .DS_Store
```
#
##### To push commited changes of a cloned repository,  use the following command:
```
$ git push origin master
```
