import React from 'react';
import { createRoot } from 'react-dom/client';
import { Root } from './presentation/Root';
import './presentation/i18n';
import { RecoilRoot } from 'recoil';

const rootElement = document.getElementById('root');
const root = createRoot(rootElement!);

root.render(<Root />);
