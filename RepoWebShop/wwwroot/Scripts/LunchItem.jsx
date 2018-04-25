const STATES = [
    { value: 'australian-capital-territory', label: 'Australian Capital Territory', className: 'State-ACT' },
    { value: 'new-south-wales', label: 'New South Wales', className: 'State-NSW' },
    { value: 'victoria', label: 'Victoria', className: 'State-Vic' },
    { value: 'queensland', label: 'Queensland', className: 'State-Qld' },
    { value: 'western-australia', label: 'Western Australia', className: 'State-WA' },
    { value: 'south-australia', label: 'South Australia', className: 'State-SA' },
    { value: 'tasmania', label: 'Tasmania', className: 'State-Tas' },
    { value: 'northern-territory', label: 'Northern Territory', className: 'State-NT' },
];

var StatesField = class InputControl extends React.Component {

    propTypes = {
        label: PropTypes.string,
        searchable: PropTypes.bool,
    }

    getDefaultProps = () => {
        return {
            label: 'States:',
            searchable: true,
        }
    }

    getInitialState = () => {
        return {
            country: 'AU',
            disabled: false,
            searchable: this.props.searchable,
            selectValue: 'new-south-wales',
            clearable: true,
            rtl: false,
        };
    }

    clearValue = (e) => {
        this.select.setInputValue('');
    }

    updateValue = (newValue) => {
        this.setState({
            selectValue: newValue,
        });
    }

    focusStateSelect = () => {
        this.refs.stateSelect.focus();
    }

    render() {
        var options = STATES;
        var Data = ['this', 'example', 'isnt', 'funny'],
            MakeItem = function (X) {
                return <option>{X}</option>;
            };

        return (
            <div className="section">
                <h3 className="section-heading">{this.props.label}</h3>
                <Select
                    id="state-select"
                    ref={(ref) => { this.select = ref; }}
                    onBlurResetsInput={false}
                    onSelectResetsInput={false}
                    autoFocus
                    options={options}
                    simpleValue
                    //clearable={this.state.clearable}
                    name="selected-state"
                    disabled={false}
                    //value={this.state.selectValue}
                    onChange={this.updateValue}
                    //rtl={this.state.rtl}
                    searchable={true}
                />
                <button style={{ marginTop: '15px' }} type="button" onClick={this.focusStateSelect}>Focus Select</button>
                <button style={{ marginTop: '15px' }} type="button" onClick={this.clearValue}>Clear Value</button>


            </div>
        );
    }
}