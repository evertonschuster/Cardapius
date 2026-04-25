import React from 'react';
import { Question, UserAnswer } from '../types';
import { FeedbackPanel } from './FeedbackPanel';

interface Props {
  questions: Question[];
  answers: UserAnswer[];
  onRestart: () => void;
}

export const ReviewScreen = ({ questions, answers, onRestart }: Props) => (
  <section className="review-screen">
    <div className="card">
      <h2>🔎 Revisão completa da prova</h2>
      <p>Veja o gabarito, sua escolha e explicações de cada alternativa.</p>
      <button type="button" className="main-btn" onClick={onRestart}>Voltar ao início</button>
    </div>

    {questions.map((question) => {
      const answer = answers.find((item) => item.questionId === question.id);
      if (!answer) return null;

      return (
        <div key={question.id} className="review-item">
          <div className="card compact">
            <h3>Questão {question.id}</h3>
            <p>{question.statement}</p>
          </div>
          <FeedbackPanel question={question} selectedOptionId={answer.selectedOptionId} mode="estudo" />
        </div>
      );
    })}
  </section>
);
