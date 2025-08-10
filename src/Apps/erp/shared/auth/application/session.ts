export interface SessionState {
  username: string;
  roles: string[];
  token: string;
}

export type SessionAction =
  | { type: 'SET'; payload: SessionState }
  | { type: 'CLEAR' };

export const initialSessionState: SessionState = {
  username: '',
  roles: [],
  token: ''
};

export const sessionReducer = (
  state: SessionState,
  action: SessionAction
): SessionState => {
  switch (action.type) {
    case 'SET':
      return action.payload;
    case 'CLEAR':
      return initialSessionState;
    default:
      return state;
  }
};
