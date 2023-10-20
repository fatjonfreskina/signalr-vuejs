<template>
  <div class="row">
    <h1> Total users: {{ totalUsers }}</h1>
    <button @click="addOne">Add One</button>
    <p>{{ counterButton }}</p>
    <button @click="sendUpdate">Send update</button>
    <p>Need update:{{ needsUpdate }}</p>
    <input type="text" v-model="userMockId" placeholder="User id">
    <input type="text" v-model="receiverId" placeholder="Receiver id">
    <p style="white-space: pre-line;">{{ message }}</p>
<textarea v-model="message" placeholder="Message to send"></textarea>
<button @click="sendMessage">Send Message to user</button>

  </div>
</template>

<script>
import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
  .withUrl(`https://localhost:7158/chatHub/`)
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

export default {
  name: 'HelloWorld',

  data() 
  {
    return {
      counterButton : 0, 
      totalUsers:0,
      needsUpdate:false,
      userMockId: '',
      receiverId: '',
      message: ''
    }
  },

  created()
  {
    this.start();

    connection.on("testGetReceived", (type) =>{
      console.log(`Received from get: ${type}`);
    })

    connection.on("DirectMessageReceived", (user, message) =>{
      console.log(`Received: ${message} from ${user}`);
    })

    connection.on("UpdateMenuReceived", (value) => {
      this.needsUpdate = value;
      console.log(`Received update: ${value}`);
    })

    connection.on("addOneReceived", (value) => {
      this.counterButton = value;
      console.log(`Received: ${value}`);
    });

    connection.on("updateTotalUsers", (users, pressed) =>{
      this.totalUsers = users;
      this.counterButton = pressed;
      console.log(`Total users: ${users}`)
    })
  },
  mounted() 
  {
    
  },

  methods: 
  {
    async start(){
      try 
      {
        await connection.start();
        console.log("SignalR Connected.");
      } 
      catch (err) 
      {
          console.log(`Signalr connection error: ${err}`);
      }
    },

    addOne(){
      this.counterButton++;
      connection.send("AddOne")
    },

    sendUpdate(){
      // backend riceve ok
      console.log("Sending update");
      connection.send("UpdateMenu");
    },

    sendMessage() {

    }
  },
  
  props: {
    msg: String
  }
}

</script>
