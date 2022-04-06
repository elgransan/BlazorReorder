# Blazor Reorder

A Drag and Drop sortable list built in Blazor. It only uses javascript for trigger events and get element information (that in NET 6 it's impossible to do it direct from Blazor)

The Reorder Component is a RCL so you can install it as nuget package or you can reference the source code project

Example code:

    <div class="card">
        <Reorder Items="list" TItem="ListItem">
            <div class="mb-2 mx-2">
                <h5>@context.title</h5>
                <p>@context.details <a href="@context.url">Go</a></p>      
            </div>
        </Reorder>
    </div>
    
    @code
    {
        public List<ListItem> list = new()
        {
            new ListItem("Google", "https://google.com", "Again looking for a bug ..."),
            new ListItem("StackOverflow", "https://stackoverflow.com", "Could be this the solution?"),
            new ListItem("GitHub", "https://github.com", "Let's steal awesome code"),
            new ListItem("Twitter", "https://twitter.com", "What a genious am I"),
            new ListItem("Another", "https://another.com", "The solution must be somewhere!!!")
        };
    }

Here is an example:

![sortable](https://user-images.githubusercontent.com/9949584/161866643-fff9989b-ca23-475d-83e0-3d80b1a77740.gif)

## Roadmap

- [x] Basic funtionality
- [x] Component RCL
- [x] Easy Restyling
- [ ] Drag & Drop between lists
