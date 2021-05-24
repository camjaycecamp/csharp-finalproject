/* cameron campbell
 * c#
 * spring 2021
 * personally controlled health record application
 */

Welcome to the Personally Controlled Health Record application!

The executable for this application is located in the same directory as this readme!

[GETTING STARTED]
When you launch the application, you'll be greeted by a login screen prompting for
a username and password. if you have an existing account, simply enter the username
and password (both are case sensitive) and click 'Login' to continue to your personal
health record. Alternatively, if you'd like to register as a new patient, click the
'Register' button to be brought to a registration form.

If you'd simply like to test out the capabilities of the program before you make your
own account, there are two test accounts available:

GUEST ACCOUNT:
Username: guest
Password: gu3$t

EXAMPLE ACCOUNT:
Username: joeysmith60
Password: qwerty1960

The example account has all fields in to demonstrate the capabilities of the application.
For more details, you can hover over each button in the login and registration forms to
read a tooltip about its function.


[PERSONAL HEALTH RECORD]
Once you've logged in with a new or existing account, you'll enter the personal health
record form. By default, you'll be placed in the 'Personal Details' tab, where you can
view, edit, and save details not directly related to your medical record, as well as
assign and change your profile picture. Hovering over each link label produces a
tooltip giving a brief description of what action will be taken should the link label
be enabled (glowing a bright blue) when you click on it.

The general rule of thumb for the 'Edit', 'Save' and 'Cancel' link labels is as follows:

- 'Edit' link labels enable an 'editing mode', where the fields of the group the 'Edit'
   link label belongs to will become able to be edited. During this editing mode, the
  'Save' and 'Cancel' link labels will be enabled, but any changes made to the fields
   are only temporary until either of the formerly-mentioned link labels are selected.

- 'Save' link labels will overwrite the previous information in each field with what
   information they currently hold. This will also end the editing mode.

- 'Cancel' link labels will undo any changes to information within each field, restoring
   them to their last-saved values. This will also end the editing mode.

The 'Change Profile Picture' will open a file dialog, allowing you to choose a new profile
picture from your system files before stretching it, displaying it, and saving it.

The Medical Details tab also houses multiple lists, each with their own items and link
labels. the general rule of thumb for the 'Add' and 'Remove Selected' link labels is
as follows:

-  'Add' link labels will, when the details group is in editing mode, add a new entry
    to the details group's list with values that match those of the fields above the
   'Add' link label.

-  'Remove Selected' link labels will, when the details group is in editing mode,
    delete the currently selected (highlighted blue) item from the list.

Neither of these changes will truly take effect until the 'Save' link label for the
corresponding details group is selected, and are undone is the details group's 'Cancel'
link label is selected.

You can also expand the details of each item in a list by double clicking on the item.
This will display a popup showing the full details of the selected item.


With this information, you should now understand the basics of the Personally Controlled
Health Record application. Now you're ready to secure good health and a brighter tomorrow!