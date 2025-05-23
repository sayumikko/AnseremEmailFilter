async function submitEmail() {
  const to = document.getElementById("toInput").value;
  const copy = document.getElementById("copyInput").value;

  const response = await fetch("/email/filter", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ to, copy }),
  });

  const result = await response.json();
  document.getElementById("result").innerHTML = `
    <b>To:</b> ${result.to}<br/>
    <b>Copy:</b> ${result.copy}
  `;
}
