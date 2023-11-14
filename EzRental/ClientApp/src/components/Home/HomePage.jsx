import React from 'react';
import { Card } from 'antd';

const cardData = [
  { title: 'Card 1', content: 'Content for Card 1' },
  { title: 'Card 2', content: 'Content for Card 2' },
  // Add more data as needed
];


export default function CardArray() {
  return (
    <div style={{ display: 'flex', flexWrap: 'wrap', gap: '16px' }}>
      {cardData.map((card, index) => (
        <Card key={index} title={card.title} style={{ width: 300 }}>
          <p>{card.content}</p>
        </Card>
      ))}
    </div>
  );
};
