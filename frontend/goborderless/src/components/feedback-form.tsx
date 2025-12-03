"use client";
import { FormEvent } from "react";

export default function FeedbackForm()
{
  async function handleSumit(event: FormEvent<HTMLFormElement>): Promise<void> {
    event.preventDefault();
    const form = event.currentTarget;
    const formData = new FormData(event.currentTarget);
    const rating = formData.get("rating");
    const comment = formData.get("comment");
    
    const res = await fetch("http://localhost:5201/api/feedback", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ rating, comment }),
    });

    if (res.ok) {
      alert("Feedback submitted successfully!");
      form.reset();
    } else {
      alert("Failed to submit feedback.");
    }

  }

    return (
        <form onSubmit={handleSumit} className="flex flex-col border">
      <input name="rating" type="number" placeholder="1 -5" required min={1} max={5}/>
      <textarea name="comment" placeholder="leave some comment" required />
      <button type="submit" className="border bg-amber-800">Submit</button>
    </form>
    )
}