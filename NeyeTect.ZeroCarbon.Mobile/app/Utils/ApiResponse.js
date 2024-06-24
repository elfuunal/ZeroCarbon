class ApiResponse {
    constructor(isSuccess, data, message) {
      this.isSuccess = isSuccess;
      this.data = data;
      this.message = message;
    }
  }

  export default ApiResponse;