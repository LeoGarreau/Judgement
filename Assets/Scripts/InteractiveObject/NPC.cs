﻿using UnityEngine;
using System.Collections;
//using System;

public class NPC : InteractiveObject {

    public TextBoxManager m_textBox;
	public string[] dialogs;
    public bool reveal;
	public int okay;
	public AssociateTextNPC assosText;
	int type;
	public PlayerController player;

    // Use this for initialization
    public void Start()
    {
        m_textBox = FindObjectOfType<TextBoxManager>();
        assosText = FindObjectOfType<AssociateTextNPC>();
		player = FindObjectOfType<PlayerController>();
        okay = 0;
		type= Random.Range (1, 4);
		dialogs = assosText.GenerateDial (type);
		Debug.Log (type);

    }

    void SetNPC(string[] dials, bool rev)
    {
        reveal = rev;
        dialogs = dials;
    }

    public void SetReveal(bool val)
    {
        reveal = val;
    }

    public bool GetReveal()
    {
        return reveal;
    }

    internal void SetRenderer(bool v)
    {
        GetComponent<Renderer>().enabled = v;
    }

    // Update is called once per frame
    public void Update () {
		if (okay == 1)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				m_textBox.ImportDialog(dialogs,true);
				okay = 2;
			}
		}
			
	}

    public override void Interact()
    {
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(okay !=2)
			okay = 1;

	}

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(m_textBox.choix);
        if (m_textBox.choix)
        {
            GetComponent<Renderer>().gameObject.SetActive(false);
            m_textBox.choix = false;
			if (type == 1)
				player.setGood (10);
			if (type==2)
				player.setGood (5);
			if (type==3)
				player.setGood (-10);
			if (type==4)
				player.setGood (-5);
        }
    }

    public void setTextBox(TextBoxManager txtBox)
    {
        this.m_textBox = txtBox;
    }

    public void setAssocTxt(AssociateTextNPC assosTxt)
    {
        this.assosText = assosTxt;
    }

}
