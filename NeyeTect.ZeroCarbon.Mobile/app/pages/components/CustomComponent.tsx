import React, { Component } from 'react';
import { View, Text, Button } from 'react-native';
import axios, { AxiosResponse } from 'axios';

interface CustomComponentState {
  responseData: any;
  error: string | null;
  loading: boolean;
}

class CustomComponent extends Component<any, CustomComponentState> {
  state: CustomComponentState = {
    responseData: null,
    error: null,
    loading: false
  };

  fetchData = async () => {
    try {
      this.setState({ loading: true });
      const response: AxiosResponse = await axios.get('https://api.example.com/data');
      this.setState({ responseData: response.data, loading: false });
    } catch (error) {
      // this.setState({ error: error.message, loading: false });
    }
  }

  render() {
    const { responseData, error, loading } = this.state;

    return (
      <View>
        {loading && <Text>Loading...</Text>}
        {error && <Text>Error: {error}</Text>}
        {responseData && (
          <View>
            <Text>Data: {JSON.stringify(responseData)}</Text>
          </View>
        )}
        <Button title="Fetch Data" onPress={this.fetchData} />
      </View>
    );
  }
}

export default CustomComponent;