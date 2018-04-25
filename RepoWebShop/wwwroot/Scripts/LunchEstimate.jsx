var LunchEstimate = class InputControl extends React.Component {
    // Use static properties for propTypes/defaultProps
    static propTypes = {
        title: PropTypes.string
    }

    static defaultProps = {
        title: 'Presupuesto'
    }

    // Initialize state right in the class body,
    // with a property initializer:
    state = {
        title: this.props.title || ''
    }

    // Use an arrow function to preserve the "this" binding
    // without having to bind in the constructor, or in render.
    handleTitleChange = (event) => {
        this.setState({
            title: event.target.value
        });
    }

    // In classes, functions are written without
    // the 'function' keyword. Also, notice there are no commas
    // between properties
    render() {
        return (
            <div className="container text-center">
                <h2>Presupuesto</h2>
                <div >
                    <div >
                        <strong>Título</strong>
                    </div>
                    <div >
                        <input onChange={this.handleTitleChange} value={this.state.title} className="form-control" />
                    </div>
                </div>
            </div>
        );
    }
}