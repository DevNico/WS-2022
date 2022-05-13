import React from 'react';
import { createRoot } from 'react-dom/client';
import { Root } from './presentation/Root';

const rootElement = document.getElementById('root');
const root = createRoot(rootElement!);

root.render(<Root />);
