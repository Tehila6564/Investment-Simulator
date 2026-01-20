import * as signalR from "@microsoft/signalr";

let connection: signalR.HubConnection | null = null;
let startPromise: Promise<void> | null = null;

export async function getSignalRConnection(): Promise<signalR.HubConnection> {
  if (
    connection &&
    (connection.state === signalR.HubConnectionState.Connected ||
      connection.state === signalR.HubConnectionState.Reconnecting)
  ) {
    return connection;
  }

  if (!connection) {
    connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7049/hubs/investments")
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Warning)
      .build();
  }

  if (connection.state === signalR.HubConnectionState.Disconnected) {
    if (!startPromise) {
      startPromise = connection
        .start()
        .then(() => {
          startPromise = null;
          console.log("SignalR Connected Successfully");
        })
        .catch((err) => {
          startPromise = null;
          console.error("SignalR Connection Error: ", err);
          throw err;
        });
    }
    await startPromise;
  }

  return connection;
}

export async function subscribeToUserState(
  username: string,
  onUpdate: (data: any) => void
) {
  try {
    const conn = await getSignalRConnection();

    if (conn.state !== signalR.HubConnectionState.Connected) {
      await startPromise;
    }

    if (conn.state === signalR.HubConnectionState.Connected) {
      await conn.invoke("JoinGroup", username);

      const callback = (data: any) => {
        console.log("SignalR Data Received:", data);
        onUpdate(data);
      };

      conn.on("state-updated", callback);

      return () => {
        conn.off("state-updated", callback);
      };
    }

    return () => {};
  } catch (err) {
    console.error("Failed to subscribe to SignalR updates", err);
    return () => {};
  }
}

export async function disconnectSignalR() {
  if (connection) {
    await connection.stop();
    connection = null;
    startPromise = null;
  }
}
