import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Message {
  sender: 'user' | 'bot';
  text: string;
}

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot.component.html',
  styleUrl: './chatbot.component.css'
})
export class ChatbotComponent {
  messages: Message[] = [
    { sender: 'bot', text: 'Hello! I am your chatbot. How can I help you today?' }
  ];
  userInput: string = '';

  sendMessage() {
    if (!this.userInput.trim()) return;
    this.messages.push({ sender: 'user', text: this.userInput });
    setTimeout(() => {
      this.messages.push({ sender: 'bot', text: this.getBotReply(this.userInput) });
    }, 500);
    this.userInput = '';
  }

  getBotReply(input: string): string {
    if (input.toLowerCase().includes('hello')) return 'Hi there!';
    if (input.toLowerCase().includes('help')) return 'I am here to assist you.';
    if (input.toLowerCase().includes('bye')) return 'Goodbye!';
    return "I'm just a demo bot. Ask me anything!";
  }
}
