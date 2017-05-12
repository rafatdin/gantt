# gantt
ASP.NET MVC5 web app for gantt chart generation with Resources and Tasks management available.

## SPA like UI
As the app itself is not complex, and to create Single Page Application like user interface 
it was modals. To be specific, `#DeleteModal` - for delete confirmation, which is a single built in modal,
also I use `#modalWrapper`, in which the _Create and _Edit partial views will be fetched.
This leaves a feeling of using the single page, application. Though, it isn't.
Moreover, there are Create and Edit separate views, in case if `ModelState.IsValid()` check
is not successful - incorrect view model will be sent to those views, which are separate pages.

## DAL
On the DAL there are two options currently, which can be switched if necessary:
* **IRepository** interface, which is available and has been implemented in entity repositories, but were not used in favor of UnitOfWork pattern *`/DAL/Repostories`*
* **GenericRepository** + **UnitOfWork**  pattern, which is currently used in the project *`/DAL`*

## Demo
Find the hosted app [here](http://treefer-001-site1.htempurl.com)
