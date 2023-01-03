# ☑️ ToDo List

## Legend

☐ => The task is yet to be completed  
✔️ => The task is completed successfully  
❌ => The task is dismissed

---

## Purpose

When all task are either completed or dismissed this file will be deleted and only GitHub Issues will be used.

---

☐ Consider renaming ApplicationDbContext to RoomedDbContext

☐ Extract Front Office MVC area

☐ Consider how to handle identity documents of deleted guests

1. Indicate a message that the owner of the document is deleted
2. When the guest is deleted delete all of his identity documents
3. Don't show identity documents with deleted owners

☐ Extract error messages to constants

☐ Extract ViewData and TempData keys to constants

☐ Make error messages format strings

✔️ Extend base service  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Add more methods like: ExistAsync(), CreateAsync() etc.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Make the virtual so every service can override them if needed.

☐ Consider making access modifiers more restrictive where appropriate

☐ Consider the use of "init;"

✔️ Make an partial for loading all scripts for a specified area, controller or view

✔️ Audit data layer

✔️ Audit web layer

✔️ Audit test layer

1. ✔️ Add documentation to all tests
2. ✔️ Add a comment above every test with the method signature that it is testing. Example: // GetAll(bool isReadonly, bool withDeleted)
3. ✔️ Add messages to all test assertions

✔️ Audit service layer

1. ✔️ Add documentation where it is missing
2. ✔️ Make changes were appropriate
3. ☐ Move ProjectTo{TDto} last before ToListAsync()

☐ Make creating a reservation and its reservation days in a transaction
